using AutoMapper;
using FastFood.Domain.Entities;
using FastFood.Domain.Interfaces;
using FastFood.Domain.RepoInterfaces;
using FastFood.Domain.ServiceInterfaces;
using FastFood.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastFood.Application.Services
{
    public class RestaurantService : IRestaurantService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RestaurantService(IUnitOfWork unitOfWork,
                                  IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }





        public async Task<RestaurantDto?> CheckDuplicateRestaurant(RestaurantDto restaurant)
        {
            return _mapper.Map<List<RestaurantDto>>(await _unitOfWork.Restaurant.GetAll())
                .Where(r => r.Name == restaurant.Name)
                .FirstOrDefault();
        }
    }
}
