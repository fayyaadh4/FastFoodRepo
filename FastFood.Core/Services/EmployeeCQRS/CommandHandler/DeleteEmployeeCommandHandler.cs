using AutoMapper;
using FastFood.Core.Services.EmployeeCQRS.Commands;
using FastFood.Domain.Entities;
using FastFood.Domain.Interfaces;
using FastFood.Domain.ServiceInterfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastFood.Core.Services.EmployeeCQRS.CommandHandler
{
    public class DeleteEmployeeCommandHandler : IRequestHandler<DeleteEmployeeCommand, bool>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public DeleteEmployeeCommandHandler(
            IEmployeeRepository employeeRepository,
            IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }
        public async Task<bool> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
        {
            if (!await _employeeRepository.EmployeeExists(request.EmployeeId))
                throw new Exception("Employee not found");

            var employeeToDelete = await _employeeRepository.GetEmployee(request.EmployeeId);

            return await _employeeRepository.DeleteEmployee(employeeToDelete);

        }
    }
}
