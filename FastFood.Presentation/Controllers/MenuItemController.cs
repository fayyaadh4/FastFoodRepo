
using FastFood.Core.Services.MenuItemCQRS.Commands;
using FastFood.Core.Services.MenuItemCQRS.Queries;
using FastFood.Domain.ServiceInterfaces;
using FastFood.Dto;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FastFood.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuItemController : Controller
    {
        private readonly IMenuItemService _menuItemService;
        private readonly IMediator _mediator;

        public MenuItemController(IMenuItemService menuItemService,
            IMediator mediator)
        {
            _menuItemService = menuItemService;
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(204, Type = typeof(IEnumerable<MenuItemDto>))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetMenuItems()
        {
            var query = new GetMenuItemsQuery();
            var result = await _mediator.Send(query);

            return Ok(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(204, Type = typeof(MenuItemDto))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetMenuItem(int id) {
            var query = new GetMenuItemQuery(id);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateMenuItem([FromBody] MenuItemDto createMenuItem)
        {
            var command = new CreateMenuItemCommand(createMenuItem);
            var result = await _mediator.Send(command);

            return Ok("Menu item created successfully");
        
        }

        [HttpPut("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateMenuItem(int id, [FromBody] MenuItemDto menuItemUpdate)
        {
            var command = new UpdateMenuItemCommand(id, menuItemUpdate);
            var result = await _mediator.Send(command);

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteMenuItem(int id)
        {
            var command = new DeleteMenuItemCommand(id);
            var result = await _mediator.Send(command);
            return NoContent();

        }


    }
}
