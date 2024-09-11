using AutoMapper;
using FastFood.Core.Services.RestaurantCQRS.Queries;
using FastFood.Domain.Entities;
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
    public class GetMenuItemsByRestaurantQueryHandler : IRequestHandler<GetMenuItemsByRestaurantQuery, ICollection<MenuItemDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetMenuItemsByRestaurantQueryHandler(IUnitOfWork unitOfWork,
                                  IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<ICollection<MenuItemDto>> Handle(GetMenuItemsByRestaurantQuery request, CancellationToken cancellationToken)
        {
            var menuItems = _mapper.Map<List<MenuItemDto>>(await _unitOfWork.Restaurant.GetMenuItemsByRestaurant(request.RestaurantId));

            return menuItems;
        }
    }
}
