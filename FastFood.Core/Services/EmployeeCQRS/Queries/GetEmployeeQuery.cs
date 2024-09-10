using FastFood.Dto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastFood.Core.Services.EmployeeCQRS.Queries
{
    public class GetEmployeeQuery : IRequest<EmployeeDto>
    {
        public int EmployeeId { get; }
        public GetEmployeeQuery(int id)
        {
            EmployeeId = id;
        }
    }
}
