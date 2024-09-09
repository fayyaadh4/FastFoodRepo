using FastFood.Dto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastFood.Core.Services.RestaurantCQRS.Commands
{
    public class CreateRestaurantCommand : IRequest<bool>
    {
        public RestaurantDto CreateRestaurant { get; }
        public CreateRestaurantCommand(RestaurantDto createRestaurant)
        {
            CreateRestaurant = createRestaurant;
        }
    }
}
