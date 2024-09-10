using AutoMapper;
using FastFood.Core.Services.MenuItemCQRS.Queries;
using FastFood.Domain.Interfaces;
using FastFood.Dto;
using MediatR;
using MediatR.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastFood.Core.Services.MenuItemCQRS.QueryHandler
{
    public class GetMenuItemQueryHandler : IRequestHandler<GetMenuItemQuery, MenuItemDto>
    {
        private readonly IMenuItemRepository _menuItemRepository;
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly IMapper _mapper;

        public GetMenuItemQueryHandler(IMenuItemRepository menuItemRepository,
            IRestaurantRepository restaurantRepository,
            IMapper mapper)
        {
            _menuItemRepository = menuItemRepository;
            _restaurantRepository = restaurantRepository;
            _mapper = mapper;
        }
        public async Task<MenuItemDto> Handle(GetMenuItemQuery request, CancellationToken cancellationToken)
        {
            if (!await _menuItemRepository.MenuItemExists(request.MenuItemId))
                throw new Exception("Menu item not found");

            var menuItem = _mapper.Map<MenuItemDto>(await _menuItemRepository.GetMenuItem(request.MenuItemId));


            return menuItem;
        }
    }
}
