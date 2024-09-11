using AutoMapper;
using FastFood.Core.Services.MenuItemCQRS.Queries;
using FastFood.Domain.Interfaces;
using FastFood.Domain.RepoInterfaces;
using FastFood.Dto;
using MediatR;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastFood.Core.Services.MenuItemCQRS.QueryHandler
{
    public class GetMenuItemsQueryHandler : IRequestHandler<GetMenuItemsQuery, ICollection<MenuItemDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetMenuItemsQueryHandler(IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<ICollection<MenuItemDto>> Handle(GetMenuItemsQuery request, CancellationToken cancellationToken)
        {
            var menuItems = _mapper.Map<List<MenuItemDto>>(await _unitOfWork.MenuItem.GetAll());

            Log.Information("Get Menu Items - Serilog => {@menuItems}", menuItems);

            return menuItems;
        }
    }
}
