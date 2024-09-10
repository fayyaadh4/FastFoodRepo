using FastFood.Dto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastFood.Core.Services.MenuItemCQRS.Commands
{
    public  class CreateMenuItemCommand : IRequest<bool>
    {
        public MenuItemDto MenuItem { get; }
        public CreateMenuItemCommand(MenuItemDto menuItem)
        {
            MenuItem = menuItem;
        }
    }
}
