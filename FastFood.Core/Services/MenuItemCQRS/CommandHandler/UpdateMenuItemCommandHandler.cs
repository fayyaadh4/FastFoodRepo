using AutoMapper;
using FastFood.Core.Services.MenuItemCQRS.Commands;
using FastFood.Domain.Entities;
using FastFood.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastFood.Core.Services.MenuItemCQRS.CommandHandler
{
    public class UpdateMenuItemCommandHandler : IRequestHandler<UpdateMenuItemCommand, bool>
    {
        private readonly IMenuItemRepository _menuItemRepository;
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly IMapper _mapper;

        public UpdateMenuItemCommandHandler(IMenuItemRepository menuItemRepository,
            IRestaurantRepository restaurantRepository,
            IMapper mapper)
        {
            _menuItemRepository = menuItemRepository;
            _restaurantRepository = restaurantRepository;
            _mapper = mapper;
        }
        public async Task<bool> Handle(UpdateMenuItemCommand request, CancellationToken cancellationToken)
        {
            if (request.UpdateMenuItem == null)
                throw new Exception("Body cannot be null");

            if (!await _menuItemRepository.MenuItemExists(request.MenuItemId))
                throw new Exception("Menu item already exists");

            if (request.MenuItemId != request.UpdateMenuItem.Id)
                throw new Exception("ID mismatch");

            var menuItemMap = _mapper.Map<MenuItem>(request.UpdateMenuItem);

            return await _menuItemRepository.UpdateMenuItem(menuItemMap);

        }
    }
}
