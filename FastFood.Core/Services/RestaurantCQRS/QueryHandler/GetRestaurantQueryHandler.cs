using AutoMapper;
using FastFood.Core.Services.RestaurantCQRS.Queries;
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
    public class GetRestaurantQueryHandler : IRequestHandler<GetRestaurantQuery, RestaurantDto>
    {
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly IMenuItemRepository _menuItemRepository;
        private readonly IMapper _mapper;

        public GetRestaurantQueryHandler(IRestaurantRepository restaurantRepository,
                                  IMenuItemRepository menuItemRepository,
                                  IMapper mapper)
        {
            _restaurantRepository = restaurantRepository;
            _menuItemRepository = menuItemRepository;
            _mapper = mapper;
        }
        public async Task<RestaurantDto> Handle(GetRestaurantQuery request, CancellationToken cancellationToken)
        {
            var restaurant = _mapper.Map<RestaurantDto>(await _restaurantRepository.GetRestaurant(request.RestaurantId));

            return restaurant;
        }
    }
}
