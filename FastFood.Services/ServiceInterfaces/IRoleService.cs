
using FastFood.Domain.Entities;
using FastFood.Dto;

namespace FastFood.Domain.ServiceInterfaces
{
    public interface IRoleService
    {
        Task<ICollection<RoleDto>> GetRoles();
        Task<RoleDto> GetRole(int id);
        Task<ICollection<EmployeeDto>> GetEmployeesByRole(int roleId);
        Task<bool> CreateRole(RoleDto role);
        Task<bool> UpdateRole(int roleId, RoleDto role);
        Task<bool> DeleteRole(int roleId);
        Task<RoleDto> CheckDuplicateRole(RoleDto role);
    }
}
