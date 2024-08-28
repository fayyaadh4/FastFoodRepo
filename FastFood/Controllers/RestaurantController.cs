
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
        public IActionResult GetRestaurant(int id)
        {
            if (!_restaurantRepository.RestaurantExists(id))
                return NotFound("Restaurant does not exist");

            var restaurant = _mapper.Map<Restaurant>(_restaurantRepository.GetRestaurant(id));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(restaurant);
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Restaurant>))]
        [ProducesResponseType(400)]
        public IActionResult GetRestaurants()
        {
            var restaurants = _mapper.Map<List<Restaurant>>(_restaurantRepository.GetRestaurants());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(restaurants);
        }

        [HttpGet("menuItem/{restaurantId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Restaurant>))]
        [ProducesResponseType(400)]
        public IActionResult GetMenuItemsByRestaurant(int restaurantId)
        {
            if (!_restaurantRepository.RestaurantExists(restaurantId))
                return NotFound("Restaurant does not exist");

            var menuItems = _mapper.Map<List<MenuItem>>(_restaurantRepository.GetMenuItemsByRestaurant(restaurantId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(menuItems);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateRestaurant([FromBody] RestaurantDto createRestaurant)
        {
            if (createRestaurant == null)
                return BadRequest(ModelState);

            var restaurantExists = _restaurantRepository.CheckDuplicateRestaurant(createRestaurant);

            if (restaurantExists != null)
            {
                ModelState.AddModelError("", "Restaurant already exists");
                return StatusCode(422, ModelState);
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var restaurantMap = _mapper.Map<Restaurant>(createRestaurant);

            if (!_restaurantRepository.CreateRestaurant(restaurantMap))
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
        public IActionResult UpdateRestaurant(int restaurantId,
             [FromBody] RestaurantDto updateRestaurant)
        {
            if (updateRestaurant == null)
                return BadRequest(ModelState);

            if (restaurantId != updateRestaurant.Id)
                return BadRequest(ModelState);

            if (!_restaurantRepository.RestaurantExists(restaurantId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var restaurantMap = _mapper.Map<Restaurant>(updateRestaurant);

            if (!_restaurantRepository.UpdateRestaurant(restaurantMap))
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
        public IActionResult DeleteRestaurant(int restaurantId)
        {
            if (!_restaurantRepository.RestaurantExists(restaurantId))
                return NotFound();

            var menuItemsToDelete = _menuItemRepository.GetMenuItemsByRestaurant(restaurantId);

            var restaurantsToDelete = _restaurantRepository.GetRestaurant(restaurantId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_menuItemRepository.DeleteMenuItems(menuItemsToDelete.ToList()))
            {
                ModelState.AddModelError("", "Something went wrong deleting menu Item");
                return StatusCode(500, ModelState);
            }

            if (!_restaurantRepository.DeleteRestaurant(restaurantsToDelete))
            {
                ModelState.AddModelError("", "Error deleting restaurant");
                return StatusCode(500, ModelState);
            }

            return NoContent();


        }
    }
}
