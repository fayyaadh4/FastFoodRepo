using AutoMapper;
using FastFood.Domain.Interfaces;
using FastFood.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using FastFood.Repo.Data;

namespace FastFood.Repo.Repositories
{
    public class MenuItemRepository : IMenuItemRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public MenuItemRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<bool> CreateMenuItem(MenuItem menuItem)
        {
            _context.Add(menuItem);
            return await Save();
        }

        public async Task<bool> DeleteMenuItem(MenuItem menuItem)
        {
            _context.Remove(menuItem);
            return await Save();
        }

        public async Task<bool> DeleteMenuItems(List<MenuItem> menuItems)
        {

            _context.RemoveRange(menuItems);
            return await Save();
        }

        public async Task<MenuItem?> GetMenuItem(int id)
        {
            return await _context.MenuItems.Where(m => m.Id == id).FirstOrDefaultAsync();
        }

        public async Task<ICollection<MenuItem>> GetMenuItems()
        {
            return await _context.MenuItems.ToListAsync();
        }

        public async Task<ICollection<MenuItem>> GetMenuItemsByRestaurant(int restaurantId)
        {
            return await _context.MenuItems.Where(m => m.RestaurantId == restaurantId).ToListAsync();
        }

        public async Task<bool> MenuItemExists(int id)
        {
            return await _context.MenuItems.AnyAsync(m => m.Id == id);
        }

        public async Task<bool> Save()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0;

        }

        public async Task<bool> UpdateMenuItem(MenuItem menuItem)
        {
            _context.Update(menuItem);
            return await Save();
        }
    }
}
