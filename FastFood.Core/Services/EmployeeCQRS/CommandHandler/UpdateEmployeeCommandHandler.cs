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
    public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand, bool>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IEmployeeService _employeeService;
        private readonly IMapper _mapper;

        public UpdateEmployeeCommandHandler(
            IEmployeeRepository employeeRepository,
            IEmployeeService employeeService,
            IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _employeeService = employeeService;
            _mapper = mapper;
        }
        public async Task<bool> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            if (request.UpdateEmployee == null)
                throw new Exception("Body cannot be null");

            if (request.EmployeeId != request.UpdateEmployee.Id)
                throw new Exception("ID mismatch");

            if (!await _employeeRepository.EmployeeExists(request.EmployeeId))
                throw new Exception("Employee not found");

            var duplicateEmployee = await _employeeService.CheckDuplicateEmployee(request.UpdateEmployee);

            if (duplicateEmployee != null)
            {
                throw new Exception("Employee already exists");
            }
            var employeeMap = _mapper.Map<Employee>(request.UpdateEmployee);


            return await _employeeRepository.UpdateEmployee(employeeMap);

        }
    }
}
