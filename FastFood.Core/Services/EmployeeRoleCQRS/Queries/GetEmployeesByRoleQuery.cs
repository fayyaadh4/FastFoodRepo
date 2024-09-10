using FastFood.Dto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastFood.Core.Services.EmployeeRoleCQRS.Queries
{
    public class GetEmployeesByRoleQuery : IRequest<ICollection<EmployeeDto>>
    {
        public int EmployeeRoleId { get; }
        public GetEmployeesByRoleQuery(int roleId)
        {
            EmployeeRoleId = roleId;
        }
    }
}
