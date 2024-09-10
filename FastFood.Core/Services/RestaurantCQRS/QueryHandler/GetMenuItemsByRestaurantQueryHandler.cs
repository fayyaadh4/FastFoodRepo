using AutoMapper;
using FastFood.Core.Services.RestaurantCQRS.Queries;
using FastFood.Domain.Entities;
using FastFood.Domain.Interfaces;
using FastFood.Dto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastFood.Core.Services.RestaurantCQRS.QueryHandler
{
    public class GetMenuItemsByRestaurantQueryHandler : IRequestHandler<GetMenuItemsByRestaurantQuery, ICollection<MenuItemDto>>
    {
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly IMenuItemRepository _menuItemRepository;
        private readonly IMapper _mapper;

        public GetMenuItemsByRestaurantQueryHandler(IRestaurantRepository restaurantRepository,
                                  IMenuItemRepository menuItemRepository,
                                  IMapper mapper)
        {
            _restaurantRepository = restaurantRepository;
            _menuItemRepository = menuItemRepository;
            _mapper = mapper;
        }
        public async Task<ICollection<MenuItemDto>> Handle(GetMenuItemsByRestaurantQuery request, CancellationToken cancellationToken)
        {
            var menuItems = _mapper.Map<List<MenuItemDto>>(await _restaurantRepository.GetMenuItemsByRestaurant(request.RestaurantId));

            return menuItems;
        }
    }
}
