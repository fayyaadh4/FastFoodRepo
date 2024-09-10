using AutoMapper;
using FastFood.Domain.Entities;
using FastFood.Domain.Interfaces;
using FastFood.Domain.ServiceInterfaces;
using FastFood.Dto;

namespace FastFood.Application.Services
{
    public class RoleService : IRoleService
    {
        private readonly IEmployeeRoleRepository _roleRepository;
        private readonly IMapper _mapper;

        public RoleService(IEmployeeRoleRepository roleRepository, IMapper mapper)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
        }
        public async Task<EmployeeRoleDto?> CheckDuplicateRole(EmployeeRoleDto role)
        {
            return _mapper.Map<List<EmployeeRoleDto>>(await _roleRepository.GetRoles())
                .Where(r => r.Name == role.Name)
                .FirstOrDefault();
        }
    }
}
