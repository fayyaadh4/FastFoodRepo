using FastFood.Models;

namespace FastFood.Interfaces
{
    public interface IMenuItemRepository
    {
        ICollection<MenuItem> GetMenuItems();
        MenuItem GetMenuItem(int id);
        ICollection<MenuItem> GetMenuItemsByRestaurant(int restaurantId);
        bool MenuItemExists (int id);
        bool CreateMenuItem(MenuItem menuItem);
        bool Save();
        bool UpdateMenuItem(MenuItem menuItem);
        bool DeleteMenuItem(MenuItem menuItem);
        bool DeleteMenuItems(List<MenuItem> menuItems);
    }
}
