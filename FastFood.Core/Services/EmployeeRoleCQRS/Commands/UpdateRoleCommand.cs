using FastFood.Dto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastFood.Core.Services.EmployeeRoleCQRS.Commands
{
    public class UpdateRoleCommand : IRequest<bool>
    {
        public int EmpRoleId { get; }
        public EmployeeRoleDto UpdateEmployeeRole { get; }
        public UpdateRoleCommand(int empRoleId, EmployeeRoleDto empRole)
        {
            EmpRoleId = empRoleId;
            UpdateEmployeeRole = empRole;
            
        }
    }
}
