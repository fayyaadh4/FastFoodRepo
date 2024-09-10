using FastFood.Dto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastFood.Core.Services.RestaurantCQRS.Queries
{
    public class GetMenuItemsByRestaurantQuery : IRequest<ICollection<MenuItemDto>>
    {
        public int RestaurantId { get; }
        public GetMenuItemsByRestaurantQuery(int restaurantId)
        {
            RestaurantId = restaurantId;
        }
    }
}
