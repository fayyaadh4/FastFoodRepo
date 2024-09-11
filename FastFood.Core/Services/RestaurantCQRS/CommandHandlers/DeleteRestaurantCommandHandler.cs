using AutoMapper;
using FastFood.Core.Services.RestaurantCQRS.Commands;
using FastFood.Domain.Entities;
using FastFood.Domain.Interfaces;
using FastFood.Domain.RepoInterfaces;
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
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRestaurantService _restaurantService;
        private readonly IMapper _mapper;

        public DeleteRestaurantCommandHandler(IUnitOfWork unitOfWork,
            IRestaurantService restaurantService,
                                  IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _restaurantService = restaurantService;
            _mapper = mapper;
        }

        public async Task<bool> Handle(DeleteRestaurantCommand request, CancellationToken cancellationToken)
        {
            if (!await _unitOfWork.Restaurant.Exists(request.RestaurantId))
                throw new Exception("Restaurant does not exist");

            var menuItemsToDelete = await _unitOfWork.MenuItem.GetMenuItemsByRestaurant(request.RestaurantId);

            var restaurantsToDelete = await _unitOfWork.Restaurant.GetById(request.RestaurantId);


            if ((!await _unitOfWork.MenuItem.RemoveMany(menuItemsToDelete.ToList())) && menuItemsToDelete.Count() > 0)
            {
                throw new Exception("Something went wrong deleting menu items");
            }

            return await _unitOfWork.Restaurant.Remove(restaurantsToDelete);


        }
    }
}
