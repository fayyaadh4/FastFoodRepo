using FastFood.Dto;
using FastFood.Models;
using System.Data;

namespace FastFood.Interfaces
{
    public interface IRoleRepository
    {
        ICollection<Role> GetRoles();
        Role GetRole(int id);
        bool RoleExists(int id);
        ICollection<Employee> GetEmployeesByRole(int roleId);
        bool CreateRole(Role role);

        bool Save();

        bool UpdateRole(Role role);
        bool DeleteRole(Role role);
        Role CheckDuplicateRole(RoleDto role);
    }
}
