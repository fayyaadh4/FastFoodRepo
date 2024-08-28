using AutoMapper;
using FastFood.Data;
using FastFood.Dto;
using FastFood.Interfaces;
using FastFood.Models;
using Microsoft.EntityFrameworkCore;

namespace FastFood.Repositories
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

        public MenuItem CheckDuplicateMenuItem(MenuItemDto menuItem)
        {
            var menuItemExists =  GetMenuItems()
                .Where(m => m.Name.Trim().ToUpper() == menuItem.Name.Trim().ToUpper())
                .FirstOrDefault();
            return menuItemExists;
        }

        public bool CreateMenuItem(MenuItem menuItem)
        {
            _context.Add(menuItem);
            return Save();
        }

        public bool DeleteMenuItem(MenuItem menuItem)
        {
            _context.Remove(menuItem);
            return Save();
        }

        public bool DeleteMenuItems(List<MenuItem> menuItems)
        {

            _context.RemoveRange(menuItems);
            return Save();
        }

        public MenuItem GetMenuItem(int id)
        {
            return _context.MenuItems.Where(m => m.Id == id).FirstOrDefault();
        }

        public ICollection<MenuItem> GetMenuItems()
        {
            return _context.MenuItems.ToList();
        }

        public ICollection<MenuItem> GetMenuItemsByRestaurant(int restaurantId)
        {
            return _context.MenuItems.Where(m => m.RestaurantId == restaurantId).ToList();
        }

        public bool MenuItemExists(int id)
        {
            return _context.MenuItems.Any(m => m.Id == id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;

        }

        public bool UpdateMenuItem(MenuItem menuItem)
        {
            _context.Update(menuItem);
            return Save();
        }
    }
}
