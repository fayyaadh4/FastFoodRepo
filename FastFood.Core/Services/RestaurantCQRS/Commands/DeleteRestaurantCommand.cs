using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastFood.Core.Services.RestaurantCQRS.Commands
{
    public class DeleteRestaurantCommand : IRequest<bool>
    {
        public int RestaurantId { get; }
        public DeleteRestaurantCommand(int restaurantId)
        {
            RestaurantId = restaurantId;
        }
    }
}
