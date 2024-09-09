
using FastFood.Dto;

namespace FastFood.Domain.ServiceInterfaces
{
    public interface IMenuItemService
    {
        Task<ICollection<MenuItemDto?>> GetMenuItems();
        Task<MenuItemDto?> GetMenuItem(int id);
        Task<bool> CreateMenuItem(MenuItemDto menuItem);
        Task<bool> UpdateMenuItem(int menuItemId, MenuItemDto menuItem);
        Task<bool> DeleteMenuItem(int menuItemId);
        Task<MenuItemDto?> CheckDuplicateMenuItem(MenuItemDto menuItem);
    }
}
