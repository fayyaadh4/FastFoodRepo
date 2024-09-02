using AutoMapper;
using FastFood.Domain.Entities;
using FastFood.Domain.Interfaces;
using FastFood.Domain.ServiceInterfaces;
using FastFood.Dto;

namespace FastFood.Application.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;

        public RoleService(IRoleRepository roleRepository, IMapper mapper)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
        }
        public async Task<bool> CreateRole(RoleDto role)
        {
            if (role == null)
                throw new Exception("Body cannot be null");

            var roleExists = await CheckDuplicateRole(role);

            if (roleExists != null)
            {
                throw new Exception("Role already exists");
            }

            var roleMap = _mapper.Map<Role>(role);

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

        public async Task<RoleDto> GetRole(int id)
        {
            if (!await _roleRepository.RoleExists(id))
                throw new Exception("Role not found");

            var role = _mapper.Map<RoleDto>(await _roleRepository.GetRole(id));


            return role;
        }

        public async Task<ICollection<RoleDto>> GetRoles()
        {
            var roles = _mapper.Map<List<RoleDto>>(await _roleRepository.GetRoles());


            return roles;
        }

        public async Task<bool> UpdateRole(int roleId, RoleDto role)
        {
            if (role == null)
                throw new Exception("Body cannot be null");

            if (roleId != role.Id)
                throw new Exception("ID mismatch");

            if (!await _roleRepository.RoleExists(roleId))
                throw new Exception("Role already exists");

            var roleMap = _mapper.Map<Role>(role);


            return await _roleRepository.UpdateRole(roleMap);
        }

        public async Task<RoleDto> CheckDuplicateRole(RoleDto role)
        {
            return (await GetRoles())
                .Where(r => r.Name == role.Name)
                .FirstOrDefault();
        }
    }
}
