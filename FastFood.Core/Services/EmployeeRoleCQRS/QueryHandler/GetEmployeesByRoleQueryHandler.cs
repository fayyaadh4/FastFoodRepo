using AutoMapper;
using FastFood.Core.Services.EmployeeRoleCQRS.Queries;
using FastFood.Domain.Interfaces;
using FastFood.Dto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastFood.Core.Services.EmployeeRoleCQRS.QueryHandler
{
    public class GetEmployeesByRoleQueryHandler : IRequestHandler<GetEmployeesByRoleQuery, ICollection<EmployeeDto>>
    {
        private readonly IEmployeeRoleRepository _roleRepository;
        private readonly IMapper _mapper;

        public GetEmployeesByRoleQueryHandler(IEmployeeRoleRepository roleRepository, IMapper mapper)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
        }
        public async Task<ICollection<EmployeeDto>> Handle(GetEmployeesByRoleQuery request, CancellationToken cancellationToken)
        {
            if (!await _roleRepository.RoleExists(request.EmployeeRoleId))
                throw new Exception("Role not found");

            var employees = _mapper.Map<List<EmployeeDto>>(await _roleRepository.GetEmployeesByRole(request.EmployeeRoleId));


            return employees;
        }
    }
}
