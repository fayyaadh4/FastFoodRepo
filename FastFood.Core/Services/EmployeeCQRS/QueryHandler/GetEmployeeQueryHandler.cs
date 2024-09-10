using AutoMapper;
using FastFood.Core.Services.EmployeeCQRS.Queries;
using FastFood.Domain.Interfaces;
using FastFood.Dto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastFood.Core.Services.EmployeeCQRS.QueryHandler
{
    public class GetEmployeeQueryHandler : IRequestHandler<GetEmployeeQuery, EmployeeDto>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public GetEmployeeQueryHandler(
            IEmployeeRepository employeeRepository,
            IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }
        public async Task<EmployeeDto> Handle(GetEmployeeQuery request, CancellationToken cancellationToken)
        {
            if (!await _employeeRepository.EmployeeExists(request.EmployeeId))
                throw new Exception("Employee not found");

            var employee = _mapper.Map<EmployeeDto>(await _employeeRepository.GetEmployee(request.EmployeeId));


            return employee;
        }
    }
}
