using AutoMapper;
using FastFood.Core.Services.MenuItemCQRS.Commands;
using FastFood.Domain.Entities;
using FastFood.Domain.Interfaces;
using FastFood.Domain.RepoInterfaces;
using FastFood.Domain.ServiceInterfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastFood.Core.Services.MenuItemCQRS.CommandHandler
{
    public class CreateMenuItemCommandHandler : IRequestHandler<CreateMenuItemCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMenuItemService _menuItemService;
        private readonly IMapper _mapper;

        public CreateMenuItemCommandHandler(IUnitOfWork unitOfWork,
            IMenuItemService menuItemService,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _menuItemService = menuItemService;
            _mapper = mapper;
        }
        public async Task<bool> Handle(CreateMenuItemCommand request, CancellationToken cancellationToken)
        {

            if (request.MenuItem == null)
                throw new Exception("Body cannot be empty");

            if (!await _unitOfWork.Restaurant.Exists(request.MenuItem.RestaurantId))
                throw new Exception("Restaurant does not exist");

            var menuItemExists = await _menuItemService.CheckDuplicateMenuItem(request.MenuItem);

            if (menuItemExists != null)
            {

                throw new Exception("Menu item already exists");
            }

            var menuItemMap = _mapper.Map<MenuItem>(request.MenuItem);

            return await _unitOfWork.MenuItem.Add(menuItemMap);

        }
    }
}
