
using FastFood.Core.Services.RestaurantCQRS.Commands;
using FastFood.Core.Services.RestaurantCQRS.Queries;
using FastFood.Domain.ServiceInterfaces;
using FastFood.Dto;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FastFood.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantController : Controller
    {
        private readonly IRestaurantService _restaurantService;
        private readonly IMediator _mediatr;

        public RestaurantController(IRestaurantService restaurantService, IMediator mediatr)
        {
            _restaurantService = restaurantService;
            _mediatr = mediatr;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(RestaurantDto))]
        [ProducesResponseType(400)]
        // will cache for 60s
        [ResponseCache(Duration = 60)]
        public async Task<IActionResult> GetRestaurant(int id)
        {
            var query = new GetRestaurantQuery(id);
            var restaurant = await _mediatr.Send(query);

            return Ok(restaurant);
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<RestaurantDto>))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetRestaurants()
        {
            // specifiying the query that i have for this endpoint
            var query = new GetRestaurantsQuery();

            var result = await _mediatr.Send(query);

            return Ok(result);
        }

        [HttpGet("menuItem/{restaurantId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<MenuItemDto>))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetMenuItemsByRestaurant(int restaurantId)
        {
            var query = new GetMenuItemsByRestaurantQuery(restaurantId);
            var result = await _mediatr.Send(query);
            
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateRestaurant([FromBody] RestaurantDto createRestaurant)
        {
            var command = new CreateRestaurantCommand(createRestaurant);

            var result = await _mediatr.Send(command);

            return result ? Ok("Restaurant successfully created") : BadRequest() ;
        }

        [HttpPut("{restaurantId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateRestaurant(int restaurantId,
             [FromBody] RestaurantDto updateRestaurant)
        {
            var command = new UpdateRestaurantCommand(restaurantId, updateRestaurant);
            var result = await _mediatr.Send(command);
            return result ? NoContent() : BadRequest();
        }

        [HttpDelete("{restaurantId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        //[Authorize]
        public async Task<IActionResult> DeleteRestaurant(int restaurantId)
        {
            var command = new DeleteRestaurantCommand(restaurantId);
            var result = await _mediatr.Send(command);
            return result ? NoContent() : BadRequest();


        }
    }
}
