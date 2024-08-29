
using AutoMapper;
using FastFood.Dto;
using FastFood.Interfaces;
using FastFood.Models;
using Microsoft.AspNetCore.Mvc;

namespace FastFood.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantController : Controller
    {
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly IMenuItemRepository _menuItemRepository;
        private readonly IMapper _mapper;

        public RestaurantController(IRestaurantRepository restaurantRepository, 
            IMenuItemRepository menuItemRepository,
            IMapper mapper)
        {
            _restaurantRepository = restaurantRepository;
            _menuItemRepository = menuItemRepository;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Restaurant))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetRestaurant(int id)
        {
            if (!(await _restaurantRepository.RestaurantExists(id)))
                return NotFound("Restaurant does not exist");

            var restaurant = _mapper.Map<Restaurant>(await _restaurantRepository.GetRestaurant(id));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(restaurant);
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Restaurant>))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetRestaurants()
        {
            var restaurants = _mapper.Map<List<Restaurant>>(await _restaurantRepository.GetRestaurants());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(restaurants);
        }

        [HttpGet("menuItem/{restaurantId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Restaurant>))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetMenuItemsByRestaurant(int restaurantId)
        {
            if (!await _restaurantRepository.RestaurantExists(restaurantId))
                return NotFound("Restaurant does not exist");

            var menuItems = _mapper.Map<List<MenuItem>>(await _restaurantRepository.GetMenuItemsByRestaurant(restaurantId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(menuItems);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateRestaurant([FromBody] RestaurantDto createRestaurant)
        {
            if (createRestaurant == null)
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
            return Ok("Restaurant successfully created");
        }

        [HttpPut("{restaurantId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateRestaurant(int restaurantId,
             [FromBody] RestaurantDto updateRestaurant)
        {
            if (updateRestaurant == null)
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

            return NoContent();
        }

        [HttpDelete("{restaurantId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteRestaurant(int restaurantId)
        {
            if (!await _restaurantRepository.RestaurantExists(restaurantId))
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

            return NoContent();


        }
    }
}
