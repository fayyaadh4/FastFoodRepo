using FastFood.Dto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastFood.Core.Services.RestaurantCQRS.Queries
{
    public class GetRestaurantQuery : IRequest<RestaurantDto>
    {
        public int RestaurantId { get; }
        public GetRestaurantQuery(int restaurantId)
        {
            RestaurantId = restaurantId;
        }
    }
}
