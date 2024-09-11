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
    public class GetRestaurantsQueryHandler : IRequestHandler<GetRestaurantsQuery, ICollection<RestaurantDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetRestaurantsQueryHandler(IUnitOfWork unitOfWork,
                                  IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ICollection<RestaurantDto>> Handle(GetRestaurantsQuery request, CancellationToken cancellationToken)
        {
            var restaurants = _mapper.Map<List<RestaurantDto>>(await _unitOfWork.Restaurant.GetAll());

            return restaurants;
        }
    }
}
