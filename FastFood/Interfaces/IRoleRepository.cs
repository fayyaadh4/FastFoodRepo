using FastFood.Dto;
using FastFood.Models;
using System.Data;

namespace FastFood.Interfaces
{
    public interface IRoleRepository
    {
        Task<ICollection<Role>> GetRoles();
        Task<Role> GetRole(int id);
        Task<bool> RoleExists(int id);
        Task<ICollection<Employee>> GetEmployeesByRole(int roleId);
        Task<bool> CreateRole(Role role);

        Task<bool> Save();

        Task<bool> UpdateRole(Role role);
        Task<bool> DeleteRole(Role role);
        Task<Role> CheckDuplicateRole(RoleDto role);
    }
}
