
using FastFood.Domain.ServiceInterfaces;
using FastFood.Dto;
using Microsoft.AspNetCore.Mvc;

namespace FastFood.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantController : Controller
    {
        private readonly IRestaurantService _restaurantService;

        public RestaurantController(IRestaurantService restaurantService)
        {
            _restaurantService = restaurantService;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(RestaurantDto))]
        [ProducesResponseType(400)]
        // will cache for 60s
        [ResponseCache(Duration = 60)]
        public async Task<IActionResult> GetRestaurant(int id)
        {
            var restaurant = await _restaurantService.GetRestaurant(id);

            return Ok(restaurant);
            /*if (!(await _restaurantRepository.RestaurantExists(id)))
                return NotFound("Restaurant does not exist");

            var restaurant = _mapper.Map<Restaurant>(await _restaurantRepository.GetRestaurant(id));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(restaurant);*/
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<RestaurantDto>))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetRestaurants()
        {
            var restaurants = await _restaurantService.GetRestaurants();

            return Ok(restaurants);
            /* var restaurants = _mapper.Map<List<Restaurant>>(await _restaurantRepository.GetRestaurants());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(restaurants); */
        }

        [HttpGet("menuItem/{restaurantId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<MenuItemDto>))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetMenuItemsByRestaurant(int restaurantId)
        {
            var menuItems = await _restaurantService.GetMenuItemsByRestaurant(restaurantId);

            return Ok(menuItems);
            /* if (!await _restaurantRepository.RestaurantExists(restaurantId))
                return NotFound("Restaurant does not exist");

            var menuItems = _mapper.Map<List<MenuItem>>(await _restaurantRepository.GetMenuItemsByRestaurant(restaurantId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(menuItems); */
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateRestaurant([FromBody] RestaurantDto createRestaurant)
        {
            var restaurantCreated = await _restaurantService.CreateRestaurant(createRestaurant);
            return Ok("Restaurant successfully created");

            /* if (createRestaurant == null)
                return BadRequest(ModelState);

            var restaurantExists = await _restaurantRepository.CheckDuplicateRestaurant(createRestaurant);

            if (restaurantExists != null)
            {
                ModelState.AddModelError("", "Restaurant already exists");
                return StatusCode(422, ModelState);
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var restaurantMap = _mapper.Map<Restaurant>(createRestaurant);

            if (!await _restaurantRepository.CreateRestaurant(restaurantMap))
            {
                ModelState.AddModelError("", "Issue creating restaurant");
                return StatusCode(422, ModelState);
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok("Restaurant successfully created"); */
        }

        [HttpPut("{restaurantId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateRestaurant(int restaurantId,
             [FromBody] RestaurantDto updateRestaurant)
        {

            await _restaurantService.UpdateRestaurant(restaurantId, updateRestaurant);
            return NoContent();
            /* if (updateRestaurant == null)
                return BadRequest(ModelState);

            if (restaurantId != updateRestaurant.Id)
                return BadRequest(ModelState);

            if (!await _restaurantRepository.RestaurantExists(restaurantId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var restaurantMap = _mapper.Map<Restaurant>(updateRestaurant);

            if (!await _restaurantRepository.UpdateRestaurant(restaurantMap))
            {
                ModelState.AddModelError("", "Something went wrong while updating the restaurant");
                return StatusCode(500, ModelState);
            }

            return NoContent(); */
        }

        [HttpDelete("{restaurantId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteRestaurant(int restaurantId)
        {
            await _restaurantService.DeleteRestaurant(restaurantId);
            return NoContent();
            /* if (!await _restaurantRepository.RestaurantExists(restaurantId))
                return NotFound();

            var menuItemsToDelete = await _menuItemRepository.GetMenuItemsByRestaurant(restaurantId);

            var restaurantsToDelete = await _restaurantRepository.GetRestaurant(restaurantId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!await _menuItemRepository.DeleteMenuItems(menuItemsToDelete.ToList()))
            {
                ModelState.AddModelError("", "Something went wrong deleting menu Item");
                return StatusCode(500, ModelState);
            }

            if (!await _restaurantRepository.DeleteRestaurant(restaurantsToDelete))
            {
                ModelState.AddModelError("", "Error deleting restaurant");
                return StatusCode(500, ModelState);
            }

            return NoContent();*/


        }
    }
}
