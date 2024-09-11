using AutoMapper;
using FastFood.Core.Services.MenuItemCQRS.Queries;
using FastFood.Domain.Interfaces;
using FastFood.Domain.RepoInterfaces;
using FastFood.Dto;
using MediatR;
using MediatR.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastFood.Core.Services.MenuItemCQRS.QueryHandler
{
    public class GetMenuItemQueryHandler : IRequestHandler<GetMenuItemQuery, MenuItemDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetMenuItemQueryHandler(IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<MenuItemDto> Handle(GetMenuItemQuery request, CancellationToken cancellationToken)
        {
            if (!await _unitOfWork.MenuItem.Exists(request.MenuItemId))
                throw new Exception("Menu item not found");

            var menuItem = _mapper.Map<MenuItemDto>(await _unitOfWork.MenuItem.GetById(request.MenuItemId));


            return menuItem;
        }
    }
}
