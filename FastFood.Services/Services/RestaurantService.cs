using AutoMapper;
using FastFood.Domain.Entities;
using FastFood.Domain.Interfaces;
using FastFood.Domain.ServiceInterfaces;
using FastFood.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastFood.Application.Services
{
    public class RestaurantService : IRestaurantService
    {
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly IMenuItemRepository _menuItemRepository;
        private readonly IMapper _mapper;

        public RestaurantService(IRestaurantRepository restaurantRepository,
                                  IMenuItemRepository menuItemRepository,
                                  IMapper mapper)
        {
            _restaurantRepository = restaurantRepository;
            _menuItemRepository = menuItemRepository;
            _mapper = mapper;
        }

        public async Task<bool> CreateRestaurant(RestaurantDto restaurant)
        {
            var restaurantExists = await CheckDuplicateRestaurant(restaurant);
            if (restaurantExists != null)
            {
                throw new Exception("Restaurant Already exists");
            }

            var restaurantMap = _mapper.Map<Restaurant>(restaurant);

            return await _restaurantRepository.CreateRestaurant(restaurantMap);
        }

        public async Task<bool> DeleteRestaurant(int restaurantId)
        {

            if (!await _restaurantRepository.RestaurantExists(restaurantId))
                throw new Exception("Restaurant does not exist");

            var menuItemsToDelete = await _menuItemRepository.GetMenuItemsByRestaurant(restaurantId);

            var restaurantsToDelete = await _restaurantRepository.GetRestaurant(restaurantId);


            if ((!await _menuItemRepository.DeleteMenuItems(menuItemsToDelete.ToList())) && menuItemsToDelete.Count() > 0)
            {
                throw new Exception("Something went wrong deleting menu items");
            }

            return await _restaurantRepository.DeleteRestaurant(restaurantsToDelete);


        }

        public async Task<ICollection<MenuItemDto>> GetMenuItemsByRestaurant(int restaurantId)
        {
            var menuItems = _mapper.Map<List<MenuItemDto>>(await _restaurantRepository.GetMenuItemsByRestaurant(restaurantId));

            return menuItems;
        }

        public async Task<RestaurantDto> GetRestaurant(int id)
        {
            var restaurant = _mapper.Map<RestaurantDto>(await _restaurantRepository.GetRestaurant(id));

            return restaurant;

        }

        public async Task<ICollection<RestaurantDto>> GetRestaurants()
        {
            var restaurants = _mapper.Map<List<RestaurantDto>>(await _restaurantRepository.GetRestaurants());

            return restaurants;
        }


        public async Task<bool> UpdateRestaurant(int restaurantId, RestaurantDto restaurant)
        {
            if (restaurant == null)
                throw new Exception("Restaurant body cannot be empty");

            if (restaurantId != restaurant.Id)
                throw new Exception("Restaurant Id's need to match");

            if (!await _restaurantRepository.RestaurantExists(restaurantId))
                throw new Exception("Restaurant does not exist");


            var restaurantMap = _mapper.Map<Restaurant>(restaurant);

            return await _restaurantRepository.UpdateRestaurant(restaurantMap);

        }

        public async Task<RestaurantDto> CheckDuplicateRestaurant(RestaurantDto restaurant)
        {
            return (await GetRestaurants())
                .Where(r => r.Name == restaurant.Name)
                .FirstOrDefault();
        }
    }
}
