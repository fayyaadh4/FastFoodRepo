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
    public class GetEmployeesQueryHandler : IRequestHandler<GetEmployeesQuery, ICollection<EmployeeDto>>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public GetEmployeesQueryHandler(
            IEmployeeRepository employeeRepository,
            IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }
        public async Task<ICollection<EmployeeDto>> Handle(GetEmployeesQuery request, CancellationToken cancellationToken)
        {
            var employees = _mapper.Map<List<EmployeeDto>>(await _employeeRepository.GetEmployees());


            return employees;
        }
    }
}
