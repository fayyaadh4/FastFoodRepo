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
    public class GetRestaurantsQueryHandler : IRequestHandler<GetRestaurantsQuery, ICollection<RestaurantDto>>
    {
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly IMenuItemRepository _menuItemRepository;
        private readonly IMapper _mapper;

        public GetRestaurantsQueryHandler(IRestaurantRepository restaurantRepository,
                                  IMenuItemRepository menuItemRepository,
                                  IMapper mapper)
        {
            _restaurantRepository = restaurantRepository;
            _menuItemRepository = menuItemRepository;
            _mapper = mapper;
        }

        public async Task<ICollection<RestaurantDto>> Handle(GetRestaurantsQuery request, CancellationToken cancellationToken)
        {
            var restaurants = _mapper.Map<List<RestaurantDto>>(await _restaurantRepository.GetRestaurants());

            return restaurants;
        }
    }
}
