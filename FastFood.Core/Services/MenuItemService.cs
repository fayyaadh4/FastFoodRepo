
using AutoMapper;
using FastFood.Domain.Entities;
using FastFood.Domain.Interfaces;
using FastFood.Domain.ServiceInterfaces;
using FastFood.Dto;
using Serilog;

namespace FastFood.Application.Services
{
    public class MenuItemService : IMenuItemService
    {
        private readonly IMenuItemRepository _menuItemRepository;
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly IMapper _mapper;

        public MenuItemService(IMenuItemRepository menuItemRepository,
            IRestaurantRepository restaurantRepository,
            IMapper mapper)
        {
            _menuItemRepository = menuItemRepository;
            _restaurantRepository = restaurantRepository;
            _mapper = mapper;
        }
        public async Task<bool> CreateMenuItem(MenuItemDto menuItem)
        {
            if (menuItem == null)
                throw new Exception("Body cannot be empty");

            if (!await _restaurantRepository.RestaurantExists(menuItem.RestaurantId))
                throw new Exception("Restaurant does not exist");

            var menuItemExists = await CheckDuplicateMenuItem(menuItem);

            if (menuItemExists != null)
            {

                throw new Exception("Menu item already exists");
            }

            var menuItemMap = _mapper.Map<MenuItem>(menuItem);

            return await _menuItemRepository.CreateMenuItem(menuItemMap);

        }

        public async Task<bool> DeleteMenuItem(int menuItemId)
        {
            if (!await _menuItemRepository.MenuItemExists(menuItemId))
                throw new Exception("Menu item already exists");


            var menuItemToDelete = await _menuItemRepository.GetMenuItem(menuItemId);

            return await _menuItemRepository.DeleteMenuItem(menuItemToDelete);

        }

        public async Task<MenuItemDto?> GetMenuItem(int id)
        {
            if (!await _menuItemRepository.MenuItemExists(id))
                throw new Exception("Menu item not found");

            var menuItem = _mapper.Map<MenuItemDto>(await _menuItemRepository.GetMenuItem(id));


            return menuItem;
        }

        public async Task<ICollection<MenuItemDto?>> GetMenuItems()
    {
        var menuItems = _mapper.Map<List<MenuItemDto>>(await _menuItemRepository.GetMenuItems());

        Log.Information("Get Menu Items - Serilog => {@menuItems}", menuItems);

        return menuItems;
    }

        public async Task<bool> UpdateMenuItem(int menuItemId, MenuItemDto menuItem)
        {
            if (menuItem == null)
                throw new Exception("Body cannot be null");

            if (!await _menuItemRepository.MenuItemExists(menuItemId))
                throw new Exception("Menu item already exists");

            if (menuItemId != menuItem.Id)
                throw new Exception("ID mismatch");

            var menuItemMap = _mapper.Map<MenuItem>(menuItem);

            return await _menuItemRepository.UpdateMenuItem(menuItemMap);
        }

        public async Task<MenuItemDto?> CheckDuplicateMenuItem(MenuItemDto menuItem)
        {
            var menuItemExists = (await GetMenuItems())
                .Where(m => m.Name.Trim().ToUpper() == menuItem.Name.Trim().ToUpper())
                .FirstOrDefault();
            return menuItemExists;
        }

    }
}
