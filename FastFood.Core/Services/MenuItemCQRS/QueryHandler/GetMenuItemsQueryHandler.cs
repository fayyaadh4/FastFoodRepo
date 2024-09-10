using AutoMapper;
using FastFood.Core.Services.MenuItemCQRS.Queries;
using FastFood.Domain.Interfaces;
using FastFood.Dto;
using MediatR;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastFood.Core.Services.MenuItemCQRS.QueryHandler
{
    public class GetMenuItemsQueryHandler : IRequestHandler<GetMenuItemsQuery, ICollection<MenuItemDto>>
    {
        private readonly IMenuItemRepository _menuItemRepository;
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly IMapper _mapper;

        public GetMenuItemsQueryHandler(IMenuItemRepository menuItemRepository,
            IRestaurantRepository restaurantRepository,
            IMapper mapper)
        {
            _menuItemRepository = menuItemRepository;
            _restaurantRepository = restaurantRepository;
            _mapper = mapper;
        }
        public async Task<ICollection<MenuItemDto>> Handle(GetMenuItemsQuery request, CancellationToken cancellationToken)
        {
            var menuItems = _mapper.Map<List<MenuItemDto>>(await _menuItemRepository.GetMenuItems());

            Log.Information("Get Menu Items - Serilog => {@menuItems}", menuItems);

            return menuItems;
        }
    }
}
