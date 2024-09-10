using FastFood.Dto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastFood.Core.Services.MenuItemCQRS.Queries
{
    public class GetMenuItemQuery : IRequest<MenuItemDto>
    {
        public int MenuItemId { get; }
        public GetMenuItemQuery(int id)
        {
            MenuItemId = id;
        }
    }
}
