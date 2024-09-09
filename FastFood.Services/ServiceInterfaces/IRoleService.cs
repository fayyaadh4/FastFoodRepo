
using FastFood.Domain.Entities;
using FastFood.Dto;

namespace FastFood.Domain.ServiceInterfaces
{
    public interface IRoleService
    {
        Task<ICollection<EmployeeRoleDto>> GetRoles();
        Task<EmployeeRoleDto> GetRole(int id);
        Task<ICollection<EmployeeDto>> GetEmployeesByRole(int roleId);
        Task<bool> CreateRole(EmployeeRoleDto role);
        Task<bool> UpdateRole(int roleId, EmployeeRoleDto role);
        Task<bool> DeleteRole(int roleId);
        Task<EmployeeRoleDto?> CheckDuplicateRole(EmployeeRoleDto role);
    }
}
