using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastFood.Core.Services.EmployeeRoleCQRS.Commands
{
    public class DeleteRoleCommand : IRequest<bool>
    {
        public int EmpRoleId { get; }
        public DeleteRoleCommand(int roleId)
        {
            EmpRoleId = roleId;
        }
    }
}
