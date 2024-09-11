using AutoMapper;
using FastFood.Core.Services.RestaurantCQRS.Commands;
using FastFood.Domain.Entities;
using FastFood.Domain.Interfaces;
using FastFood.Domain.RepoInterfaces;
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
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRestaurantService _restaurantService;
        private readonly IMapper _mapper;

        public CreateRestaurantCommandHandler(IUnitOfWork unitOfWork,
            IRestaurantService restaurantService,
                                  IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _restaurantService = restaurantService;
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

             await _unitOfWork.Restaurant.Add(restaurantMap);
            return await _unitOfWork.CompleteAsync();
        }
    }
}
