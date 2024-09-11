using AutoMapper;
using FastFood.Core.Services.EmployeeCQRS.Commands;
using FastFood.Domain.Entities;
using FastFood.Domain.Interfaces;
using FastFood.Domain.RepoInterfaces;
using FastFood.Domain.ServiceInterfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastFood.Core.Services.EmployeeCQRS.CommandHandler
{
    public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmployeeService _employeeService;
        private readonly IMapper _mapper;

        public CreateEmployeeCommandHandler(
            IUnitOfWork unitOfWork,
            IEmployeeService employeeService,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _employeeService = employeeService;
            _mapper = mapper;
        }
        public async Task<bool> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            if (request.CreateEmployee == null)
                throw new Exception("Body cant be null");

            var employeeExists = await _employeeService.CheckDuplicateEmployee(request.CreateEmployee);

            if (employeeExists != null)
            {
                throw new Exception("Employee already exists");
            }


            var employeeMap = _mapper.Map<Employee>(request.CreateEmployee);


            await _unitOfWork.Employee.Add(employeeMap);
            return await _unitOfWork.CompleteAsync();

        }
    }
}
