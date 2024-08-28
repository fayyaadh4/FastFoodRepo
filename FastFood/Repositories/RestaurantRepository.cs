using AutoMapper;
using FastFood.Data;
using FastFood.Dto;
using FastFood.Interfaces;
using FastFood.Models;
using Microsoft.EntityFrameworkCore;

namespace FastFood.Repositories
{
    public class RestaurantRepository : IRestaurantRepository
    {
        private readonly DataContext _context;

        public RestaurantRepository(DataContext context)
        {
            _context = context;
        }

        public Restaurant CheckDuplicateRestaurant(RestaurantDto restaurant)
        {
            return GetRestaurants()
                .Where(r => r.Name == restaurant.Name)
                .FirstOrDefault();
        }

        public bool CreateRestaurant(Restaurant restaurant)
        {
            _context.Add(restaurant);
            return Save();
        }

        public bool DeleteRestaurant(Restaurant restaurant)
        {
            _context.Remove(restaurant);
            return Save();
        }

        public ICollection<MenuItem> GetMenuItemsByRestaurant(int restaurantId)
        {
            return _context.MenuItems.Where(m => m.RestaurantId == restaurantId).ToList();
        }

        public Restaurant GetRestaurant(int id)
        {
            return _context.Restaurants.Where(r => r.Id == id).FirstOrDefault();
        }

        public ICollection<Restaurant> GetRestaurants()
        {
            return _context.Restaurants.OrderBy(r => r.Name).ToList();
        }

        public bool RestaurantExists(int id)
        {
            return _context.Restaurants.Any(r => r.Id == id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateRestaurant(Restaurant restaurant)
        {
            _context.Update(restaurant);
            return Save();
        }
    }
}
