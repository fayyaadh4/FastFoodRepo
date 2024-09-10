using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastFood.Core.Services.EmployeeCQRS.Commands
{
    public class DeleteEmployeeCommand : IRequest<bool>
    {
        public int EmployeeId { get; }
        public DeleteEmployeeCommand(int id)
        {
            EmployeeId = id;
        }
    }
}
