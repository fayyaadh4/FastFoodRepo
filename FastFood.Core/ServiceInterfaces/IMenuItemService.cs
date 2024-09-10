
using FastFood.Dto;

namespace FastFood.Domain.ServiceInterfaces
{
    public interface IMenuItemService
    {
        Task<MenuItemDto?> CheckDuplicateMenuItem(MenuItemDto menuItem);
    }
}
