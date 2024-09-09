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
    public class DeleteRestaurantCommandHandler : IRequestHandler<DeleteRestaurantCommand, bool>
    {
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly IRestaurantService _restaurantService;
        private readonly IMenuItemRepository _menuItemRepository;
        private readonly IMapper _mapper;

        public DeleteRestaurantCommandHandler(IRestaurantRepository restaurantRepository,
            IRestaurantService restaurantService,
        IMenuItemRepository menuItemRepository,
                                  IMapper mapper)
        {
            _restaurantRepository = restaurantRepository;
            _restaurantService = restaurantService;
            _menuItemRepository = menuItemRepository;
            _mapper = mapper;
        }

        public async Task<bool> Handle(DeleteRestaurantCommand request, CancellationToken cancellationToken)
        {
            if (!await _restaurantRepository.RestaurantExists(request.RestaurantId))
                throw new Exception("Restaurant does not exist");

            var menuItemsToDelete = await _menuItemRepository.GetMenuItemsByRestaurant(request.RestaurantId);

            var restaurantsToDelete = await _restaurantRepository.GetRestaurant(request.RestaurantId);


            if ((!await _menuItemRepository.DeleteMenuItems(menuItemsToDelete.ToList())) && menuItemsToDelete.Count() > 0)
            {
                throw new Exception("Something went wrong deleting menu items");
            }

            return await _restaurantRepository.DeleteRestaurant(restaurantsToDelete);


        }
    }
}
