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
    public class GetRoleQueryHandler : IRequestHandler<GetRoleQuery, EmployeeRoleDto>
    {
        private readonly IEmployeeRoleRepository _roleRepository;
        private readonly IMapper _mapper;

        public GetRoleQueryHandler(IEmployeeRoleRepository roleRepository, IMapper mapper)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
        }
        public async Task<EmployeeRoleDto> Handle(GetRoleQuery request, CancellationToken cancellationToken)
        {
            if (!await _roleRepository.RoleExists(request.EmployeeRoleId))
                throw new Exception("Role not found");

            var role = _mapper.Map<EmployeeRoleDto>(await _roleRepository.GetRole(request.EmployeeRoleId));


            return role;
        }
    }
}
