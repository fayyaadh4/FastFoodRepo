using FastFood.Dto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastFood.Core.Services.RestaurantCQRS.Commands
{
    public class UpdateRestaurantCommand : IRequest<bool>
    {
        public int RestaurantId { get; }
        public RestaurantDto UpdateRestaurant { get; }

        public UpdateRestaurantCommand(int restaurantId, RestaurantDto updateRestaurant)
        {
            RestaurantId = restaurantId;
            UpdateRestaurant = updateRestaurant;
        }
    }
}
