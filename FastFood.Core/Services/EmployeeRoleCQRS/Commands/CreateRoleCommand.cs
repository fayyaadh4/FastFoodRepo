using FastFood.Dto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastFood.Core.Services.EmployeeRoleCQRS.Commands
{
    public class CreateRoleCommand : IRequest<bool>
    {
        public EmployeeRoleDto EmployeeRole { get; }
        public CreateRoleCommand(EmployeeRoleDto createRole)
        {
            EmployeeRole = createRole;
        }
    }
}
