﻿
using FastFood.Domain.Entities;
using FastFood.Domain.RepoInterfaces;

namespace FastFood.Domain.Interfaces
{
    public interface IRestaurantRepository : IGenericRepository<Restaurant>
    {

        Task<ICollection<MenuItem>> GetMenuItemsByRestaurant(int restaurantId);
        /* Task<ICollection<Restaurant>> GetRestaurants();
         Task<Restaurant?> GetRestaurant(int id);
         Task<bool> RestaurantExists(int id);
         Task<ICollection<MenuItem>> GetMenuItemsByRestaurant(int restaurantId);
         Task<bool> CreateRestaurant (Restaurant restaurant);

         Task<bool> Save();

         Task<bool> UpdateRestaurant(Restaurant restaurant);
         Task<bool> DeleteRestaurant(Restaurant restaurant);*/
    }
}
