using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastFood.Core.Services.MenuItemCQRS.Commands
{
    public class DeleteMenuItemCommand : IRequest<bool>
    {
        public int MenuItemId { get; }
        public DeleteMenuItemCommand(int id)
        {
            MenuItemId = id;
        }
    }
}
