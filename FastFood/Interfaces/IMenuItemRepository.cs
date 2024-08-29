using FastFood.Dto;
using FastFood.Models;

namespace FastFood.Interfaces
{
    public interface IMenuItemRepository
    {
        Task<ICollection<MenuItem>> GetMenuItems();
        Task<MenuItem> GetMenuItem(int id);
        Task<ICollection<MenuItem>> GetMenuItemsByRestaurant(int restaurantId);
        Task<bool> MenuItemExists (int id);
        Task<bool> CreateMenuItem(MenuItem menuItem);
        Task<bool> Save();
        Task<bool> UpdateMenuItem(MenuItem menuItem);
        Task<bool> DeleteMenuItem(MenuItem menuItem);
        Task<bool> DeleteMenuItems(List<MenuItem> menuItems);
        Task<MenuItem> CheckDuplicateMenuItem(MenuItemDto menuItem);
    }
}
