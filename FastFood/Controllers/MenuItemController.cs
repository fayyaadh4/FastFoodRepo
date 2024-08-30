using AutoMapper;
using FastFood.Dto;
using FastFood.Filters;
using FastFood.Interfaces;
using FastFood.Models;
using FastFood.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace FastFood.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuItemController : Controller
    {
        private readonly IMenuItemRepository _menuItemRepository;
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly IMapper _mapper;

        public MenuItemController(IMenuItemRepository menuItemRepository,
            IRestaurantRepository restauramtRepository,
            IMapper mapper)
        {
            _menuItemRepository = menuItemRepository;
            _restaurantRepository = restauramtRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(204, Type = typeof(IEnumerable<MenuItem>))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetMenuItems()
        {
            var menuItems = _mapper.Map<List<MenuItem>>(await _menuItemRepository.GetMenuItems());


            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Log.Information("Get Menu Items - Serilog => {@menuItems}", menuItems);

            return Ok(menuItems);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(204, Type = typeof(MenuItem))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetMenuItem(int id) {
            if (!await _menuItemRepository.MenuItemExists(id))
                return NotFound("Menu item not found");

            var menuItem = _mapper.Map<MenuItem>(await _menuItemRepository.GetMenuItem(id));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(menuItem);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateMenuItem([FromBody] MenuItemDto createMenuItem)
        {
            if (createMenuItem == null)
                return BadRequest(ModelState);

            if (!await _restaurantRepository.RestaurantExists(createMenuItem.RestaurantId))
                return BadRequest(ModelState);

            var menuItemExists = await _menuItemRepository.CheckDuplicateMenuItem(createMenuItem);

            if (menuItemExists != null)
            {
                ModelState.AddModelError("", "Menu Item already exists");
                return StatusCode(422, ModelState);
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var menuItemMap = _mapper.Map<MenuItem>(createMenuItem);

            if (!await _menuItemRepository.CreateMenuItem(menuItemMap))
            {
                ModelState.AddModelError("", "Issue creating menu item");
                return StatusCode(422, ModelState);
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok("Menu Item  successfully created");
        
        }

        [HttpPut("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateMenuItem(int id, [FromBody] MenuItemDto menuItemUpdate)
        {
            if (menuItemUpdate == null)
                return BadRequest(ModelState);

            if (!await _menuItemRepository.MenuItemExists(id)) 
                return NotFound("Menu Item not found");

            if (id != menuItemUpdate.Id)
                return BadRequest(ModelState);

            var menuItemMap = _mapper.Map<MenuItem>(menuItemUpdate);

            if (!await _menuItemRepository.UpdateMenuItem(menuItemMap))
            {

                ModelState.AddModelError("", "Error While updating menu item");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteMenuItem(int id)
        {
            if (!await _menuItemRepository.MenuItemExists(id))
                return NotFound();


            var menuItemToDelete = await _menuItemRepository.GetMenuItem(id);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!await _menuItemRepository.DeleteMenuItem(menuItemToDelete))
            {
                ModelState.AddModelError("", "Error deleting menu item");
                return StatusCode(500, ModelState);
            }

            return NoContent();

        }


    }
}
