
using FastFood.Domain.ServiceInterfaces;
using FastFood.Dto;
using Microsoft.AspNetCore.Mvc;

namespace FastFood.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuItemController : Controller
    {
        private readonly IMenuItemService _menuItemService;

        public MenuItemController(IMenuItemService menuItemService)
        {
            _menuItemService = menuItemService;
        }

        [HttpGet]
        [ProducesResponseType(204, Type = typeof(IEnumerable<MenuItemDto>))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetMenuItems()
        {
            var menuItems = await _menuItemService.GetMenuItems();

            return Ok(menuItems);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(204, Type = typeof(MenuItemDto))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetMenuItem(int id) {
            var menuItem = await _menuItemService.GetMenuItem(id);
            return Ok(menuItem);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateMenuItem([FromBody] MenuItemDto createMenuItem)
        {
            await _menuItemService.CreateMenuItem(createMenuItem);

            return Ok("Menu item created successfully");
        
        }

        [HttpPut("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateMenuItem(int id, [FromBody] MenuItemDto menuItemUpdate)
        {
            await _menuItemService.UpdateMenuItem(id,menuItemUpdate);

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteMenuItem(int id)
        {
            await _menuItemService.DeleteMenuItem(id);
            return NoContent();

        }


    }
}
