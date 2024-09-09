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
        public async Task<bool> CreateRole(EmployeeRoleDto role)
        {
            if (role == null)
                throw new Exception("Body cannot be null");

            var roleExists = await CheckDuplicateRole(role);

            if (roleExists != null)
            {
                throw new Exception("Role already exists");
            }

            var roleMap = _mapper.Map<EmployeeRole>(role);

            return await _roleRepository.CreateRole(roleMap);
        }

        public async Task<bool> DeleteRole(int roleId)
        {
            if (!await _roleRepository.RoleExists(roleId))
                throw new Exception("Role not found");

            var roleToDelete = await _roleRepository.GetRole(roleId);


            return await _roleRepository.DeleteRole(roleToDelete);

        }

        public async Task<ICollection<EmployeeDto>> GetEmployeesByRole(int roleId)
        {
            if (!await _roleRepository.RoleExists(roleId))
                throw new Exception("Role not found");

            var employees = _mapper.Map<List<EmployeeDto>>(await _roleRepository.GetEmployeesByRole(roleId));


            return employees;
        }

        public async Task<EmployeeRoleDto> GetRole(int id)
        {
            if (!await _roleRepository.RoleExists(id))
                throw new Exception("Role not found");

            var role = _mapper.Map<EmployeeRoleDto>(await _roleRepository.GetRole(id));


            return role;
        }

        public async Task<ICollection<EmployeeRoleDto>> GetRoles()
        {
            var roles = _mapper.Map<List<EmployeeRoleDto>>(await _roleRepository.GetRoles());


            return roles;
        }

        public async Task<bool> UpdateRole(int empRoleId, EmployeeRoleDto empRole)
        {
            if (empRole == null)
                throw new Exception("Body cannot be null");

            if (empRoleId != empRole.Id)
                throw new Exception("ID mismatch");

            if (!await _roleRepository.RoleExists(empRoleId))
                throw new Exception("Role already exists");

            var roleMap = _mapper.Map<EmployeeRole>(empRole);


            return await _roleRepository.UpdateRole(roleMap);
        }

        public async Task<EmployeeRoleDto?> CheckDuplicateRole(EmployeeRoleDto role)
        {
            return (await GetRoles())
                .Where(r => r.Name == role.Name)
                .FirstOrDefault();
        }
    }
}
