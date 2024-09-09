using AutoMapper;
using FastFood.Core.Services.RestaurantCQRS.Commands;
using FastFood.Domain.Entities;
using FastFood.Domain.Interfaces;
using FastFood.Domain.ServiceInterfaces;
using FastFood.Dto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastFood.Core.Services.RestaurantCQRS.CommandHandlers
{
    public class CreateRestaurantCommandHandler : IRequestHandler<CreateRestaurantCommand, bool>
    {
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly IRestaurantService _restaurantService;
        private readonly IMenuItemRepository _menuItemRepository;
        private readonly IMapper _mapper;

        public CreateRestaurantCommandHandler(IRestaurantRepository restaurantRepository,
            IRestaurantService restaurantService,
        IMenuItemRepository menuItemRepository,
                                  IMapper mapper)
        {
            _restaurantRepository = restaurantRepository;
            _restaurantService = restaurantService;
            _menuItemRepository = menuItemRepository;
            _mapper = mapper;
        }

        public async Task<bool> Handle(CreateRestaurantCommand request, CancellationToken cancellationToken)
        {
            var restaurantExists = await _restaurantService.CheckDuplicateRestaurant(request.CreateRestaurant);
            if (restaurantExists != null)
            {
                throw new Exception("Restaurant Already exists");
            }
            var restaurantMap = _mapper.Map<Restaurant>(request.CreateRestaurant);

            return await _restaurantRepository.CreateRestaurant(restaurantMap);
        }
    }
}
