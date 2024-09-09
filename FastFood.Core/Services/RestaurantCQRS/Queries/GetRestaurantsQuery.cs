using FastFood.Dto;
using MediatR;
using Microsoft.AspNetCore.Http.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastFood.Core.Services.RestaurantCQRS.Queries
{
    public class GetRestaurantsQuery : IRequest<ICollection<RestaurantDto>>
    {
    }
}
