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
    public class DeleteMenuItemCommandHandler: IRequestHandler<DeleteMenuItemCommand, bool>
    {
        private readonly IMenuItemRepository _menuItemRepository;
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly IMapper _mapper;

        public DeleteMenuItemCommandHandler(IMenuItemRepository menuItemRepository,
            IRestaurantRepository restaurantRepository,
            IMapper mapper)
        {
            _menuItemRepository = menuItemRepository;
            _restaurantRepository = restaurantRepository;
            _mapper = mapper;
        }

        public async Task<bool> Handle(DeleteMenuItemCommand request, CancellationToken cancellationToken)
        {
            if (!await _menuItemRepository.MenuItemExists(request.MenuItemId))
                throw new Exception("Menu item already exists");


            var menuItemToDelete = await _menuItemRepository.GetMenuItem(request.MenuItemId);

            return await _menuItemRepository.DeleteMenuItem(menuItemToDelete);

        }
    }
}
