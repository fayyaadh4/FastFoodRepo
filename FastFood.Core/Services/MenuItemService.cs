
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


        public async Task<MenuItemDto?> CheckDuplicateMenuItem(MenuItemDto menuItem)
        {
            var menuItemExists = _mapper.Map<List<MenuItemDto>>(await _menuItemRepository.GetMenuItems())
                .Where(m => m.Name.Trim().ToUpper() == menuItem.Name.Trim().ToUpper())
                .FirstOrDefault();
            return menuItemExists;
        }

    }
}
