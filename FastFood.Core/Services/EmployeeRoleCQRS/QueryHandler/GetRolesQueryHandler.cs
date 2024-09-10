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
    public class GetRolesQueryHandler : IRequestHandler<GetRolesQuery, ICollection<EmployeeRoleDto>>
    {
        private readonly IEmployeeRoleRepository _roleRepository;
        private readonly IMapper _mapper;

        public GetRolesQueryHandler(IEmployeeRoleRepository roleRepository, IMapper mapper)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
        }
        public async Task<ICollection<EmployeeRoleDto>> Handle(GetRolesQuery request, CancellationToken cancellationToken)
        {
            var roles = _mapper.Map<List<EmployeeRoleDto>>(await _roleRepository.GetRoles());

            return roles;
        }
    }
}
