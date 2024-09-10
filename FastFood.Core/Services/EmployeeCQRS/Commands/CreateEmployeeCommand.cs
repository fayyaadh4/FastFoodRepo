using FastFood.Dto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastFood.Core.Services.EmployeeCQRS.Commands
{
    public class CreateEmployeeCommand : IRequest<bool>
    {
        public EmployeeDto CreateEmployee { get; }
        public CreateEmployeeCommand(EmployeeDto employee)
        {
            CreateEmployee = employee;
        }
    }
}
