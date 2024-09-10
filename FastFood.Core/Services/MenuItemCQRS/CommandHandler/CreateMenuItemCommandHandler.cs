using AutoMapper;
using FastFood.Core.Services.MenuItemCQRS.Commands;
using FastFood.Domain.Entities;
using FastFood.Domain.Interfaces;
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
        private readonly IMenuItemRepository _menuItemRepository;
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly IMenuItemService _menuItemService;
        private readonly IMapper _mapper;

        public CreateMenuItemCommandHandler(IMenuItemRepository menuItemRepository,
            IRestaurantRepository restaurantRepository,
            IMenuItemService menuItemService,
            IMapper mapper)
        {
            _menuItemRepository = menuItemRepository;
            _restaurantRepository = restaurantRepository;
            _menuItemService = menuItemService;
            _mapper = mapper;
        }
        public async Task<bool> Handle(CreateMenuItemCommand request, CancellationToken cancellationToken)
        {

            if (request.MenuItem == null)
                throw new Exception("Body cannot be empty");

            if (!await _restaurantRepository.RestaurantExists(request.MenuItem.RestaurantId))
                throw new Exception("Restaurant does not exist");

            var menuItemExists = await _menuItemService.CheckDuplicateMenuItem(request.MenuItem);

            if (menuItemExists != null)
            {

                throw new Exception("Menu item already exists");
            }

            var menuItemMap = _mapper.Map<MenuItem>(request.MenuItem);

            return await _menuItemRepository.CreateMenuItem(menuItemMap);

        }
    }
}
