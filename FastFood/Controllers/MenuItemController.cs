using AutoMapper;
using FastFood.Dto;
using FastFood.Filters;
using FastFood.Interfaces;
using FastFood.Models;
using FastFood.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult GetMenuItems()
        {
            var menuItems = _mapper.Map<List<MenuItem>>(_menuItemRepository.GetMenuItems());


            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(menuItems);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(204, Type = typeof(MenuItem))]
        [ProducesResponseType(400)]
        public IActionResult GetMenuItem(int id) {
            if (!_menuItemRepository.MenuItemExists(id))
                return NotFound("Menu item not found");

            var menuItem = _mapper.Map<MenuItem>(_menuItemRepository.GetMenuItem(id));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(menuItem);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateMenuItem([FromBody] MenuItemDto createMenuItem)
        {
            if (createMenuItem == null)
                return BadRequest(ModelState);

            if (!_restaurantRepository.RestaurantExists(createMenuItem.RestaurantId))
                return BadRequest(ModelState);

            var menuItemExists = _menuItemRepository.CheckDuplicateMenuItem(createMenuItem);

            if (menuItemExists != null)
            {
                ModelState.AddModelError("", "Menu Item already exists");
                return StatusCode(422, ModelState);
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var menuItemMap = _mapper.Map<MenuItem>(createMenuItem);

            if (!_menuItemRepository.CreateMenuItem(menuItemMap))
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
        public IActionResult UpdateMenuItem(int id, [FromBody] MenuItemDto menuItemUpdate)
        {
            if (menuItemUpdate == null)
                return BadRequest(ModelState);

            if (!_menuItemRepository.MenuItemExists(id)) 
                return NotFound("Menu Item not found");

            if (id != menuItemUpdate.Id)
                return BadRequest(ModelState);

            var menuItemMap = _mapper.Map<MenuItem>(menuItemUpdate);

            if (!_menuItemRepository.UpdateMenuItem(menuItemMap))
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
        public IActionResult DeleteMenuItem(int id)
        {
            if (!_menuItemRepository.MenuItemExists(id))
                return NotFound();


            var menuItemToDelete = _menuItemRepository.GetMenuItem(id);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_menuItemRepository.DeleteMenuItem(menuItemToDelete))
            {
                ModelState.AddModelError("", "Error deleting menu item");
                return StatusCode(500, ModelState);
            }

            return NoContent();

        }


    }
}
