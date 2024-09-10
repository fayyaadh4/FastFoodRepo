using FastFood.Dto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastFood.Core.Services.EmployeeRoleCQRS.Queries
{
    public class GetRoleQuery : IRequest<EmployeeRoleDto>
    {
        public int EmployeeRoleId { get; }
        public GetRoleQuery(int id)
        {
            EmployeeRoleId = id;
        }
    }
}
