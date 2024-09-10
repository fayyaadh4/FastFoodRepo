using FastFood.Dto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastFood.Core.Services.MenuItemCQRS.Commands
{
    public class UpdateMenuItemCommand : IRequest<bool>
    {
        public int MenuItemId { get; }
        public MenuItemDto UpdateMenuItem { get; }

        public UpdateMenuItemCommand(int id, MenuItemDto updateMenuItem)
        {
            MenuItemId = id;
            UpdateMenuItem = updateMenuItem;
        }
    }
}
