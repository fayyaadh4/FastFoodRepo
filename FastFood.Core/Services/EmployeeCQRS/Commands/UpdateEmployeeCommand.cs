using FastFood.Dto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastFood.Core.Services.EmployeeCQRS.Commands
{
    public class UpdateEmployeeCommand : IRequest<bool>
    {
        public int EmployeeId { get; }
        public EmployeeDto UpdateEmployee { get; }
        public UpdateEmployeeCommand(int id, EmployeeDto employee)
        {
            EmployeeId = id;
            UpdateEmployee = employee;
        }
    }
}
