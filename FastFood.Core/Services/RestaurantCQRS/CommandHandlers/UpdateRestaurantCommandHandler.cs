using AutoMapper;
using FastFood.Core.Services.RestaurantCQRS.Commands;
using FastFood.Domain.Entities;
using FastFood.Domain.Interfaces;
using FastFood.Domain.ServiceInterfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastFood.Core.Services.RestaurantCQRS.CommandHandlers
{
    public class UpdateRestaurantCommandHandler : IRequestHandler<UpdateRestaurantCommand, bool>
    {
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly IRestaurantService _restaurantService;
        private readonly IMenuItemRepository _menuItemRepository;
        private readonly IMapper _mapper;

        public UpdateRestaurantCommandHandler(IRestaurantRepository restaurantRepository,
            IRestaurantService restaurantService,
        IMenuItemRepository menuItemRepository,
                                  IMapper mapper)
        {
            _restaurantRepository = restaurantRepository;
            _restaurantService = restaurantService;
            _menuItemRepository = menuItemRepository;
            _mapper = mapper;
        }

        public async Task<bool> Handle(UpdateRestaurantCommand request, CancellationToken cancellationToken)
        {
            if (request.UpdateRestaurant == null)
                throw new Exception("Restaurant body cannot be empty");

            if (request.RestaurantId != request.UpdateRestaurant.Id)
                throw new Exception("Restaurant Id's need to match");

            if (!await _restaurantRepository.RestaurantExists(request.RestaurantId))
                throw new Exception("Restaurant does not exist");
            var restaurantMap = _mapper.Map<Restaurant>(request.UpdateRestaurant);

            return await _restaurantRepository.UpdateRestaurant(restaurantMap);
        }
    }
}
