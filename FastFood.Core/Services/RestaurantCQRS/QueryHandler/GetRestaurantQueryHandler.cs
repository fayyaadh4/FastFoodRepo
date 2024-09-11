using AutoMapper;
using FastFood.Core.Services.RestaurantCQRS.Queries;
using FastFood.Domain.Interfaces;
using FastFood.Domain.RepoInterfaces;
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
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetRestaurantQueryHandler(IUnitOfWork unitOfWork,
                                  IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<RestaurantDto> Handle(GetRestaurantQuery request, CancellationToken cancellationToken)
        {
            var restaurant = _mapper.Map<RestaurantDto>(await _unitOfWork.Restaurant.GetById(request.RestaurantId));

            return restaurant;
        }
    }
}
