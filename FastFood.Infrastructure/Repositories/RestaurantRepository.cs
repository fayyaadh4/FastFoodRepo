using FastFood.Domain.Interfaces;
using FastFood.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using FastFood.Repo.Data;

namespace FastFood.Repo.Repositories
{
    public class RestaurantRepository : IRestaurantRepository
    {
        private readonly DataContext _context;

        public RestaurantRepository(DataContext context)
        {
            _context = context;
        }


        public async Task<bool> CreateRestaurant(Restaurant restaurant)
        {
            _context.Add(restaurant);
            return await Save();
        }

        public async Task<bool> DeleteRestaurant(Restaurant restaurant)
        {
            _context.Remove(restaurant);
            return await Save();
        }

        public async Task<ICollection<MenuItem>> GetMenuItemsByRestaurant(int restaurantId)
        {
            return await _context.MenuItems.Where(m => m.RestaurantId == restaurantId).ToListAsync();
        }

        public async Task<Restaurant?> GetRestaurant(int id)
        {
            return await _context.Restaurants.Where(r => r.Id == id).FirstOrDefaultAsync();
        }

        public async Task<ICollection<Restaurant>> GetRestaurants()
        {
            return await _context.Restaurants.OrderBy(r => r.Name).ToListAsync();
        }

        public async Task<bool> RestaurantExists(int id)
        {
            return await _context.Restaurants.AnyAsync(r => r.Id == id);
        }

        public async Task<bool> Save()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0;
        }

        public async Task<bool> UpdateRestaurant(Restaurant restaurant)
        {
            _context.Update(restaurant);
            return await Save();
        }
    }
}
