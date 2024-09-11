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
    public class UpdateRestaurantCommandHandler : IRequestHandler<UpdateRestaurantCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRestaurantService _restaurantService;
        private readonly IMapper _mapper;

        public UpdateRestaurantCommandHandler(IUnitOfWork unitOfWork,
            IRestaurantService restaurantService,
                                  IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _restaurantService = restaurantService;
            _mapper = mapper;
        }

        public async Task<bool> Handle(UpdateRestaurantCommand request, CancellationToken cancellationToken)
        {
            if (request.UpdateRestaurant == null)
                throw new Exception("Restaurant body cannot be empty");

            if (request.RestaurantId != request.UpdateRestaurant.Id)
                throw new Exception("Restaurant Id's need to match");

            if (!await _unitOfWork.Restaurant.Exists(request.RestaurantId))
                throw new Exception("Restaurant does not exist");
            var restaurantMap = _mapper.Map<Restaurant>(request.UpdateRestaurant);

            return await _unitOfWork.Restaurant.Update(restaurantMap);
        }
    }
}
