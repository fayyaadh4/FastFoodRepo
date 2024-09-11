using FastFood.Domain.Entities;
using FastFood.Domain.RepoInterfaces;

namespace FastFood.Domain.Interfaces
{
    public interface IEmployeeRoleRepository : IGenericRepository<EmployeeRole>
    {
        Task<ICollection<Employee>> GetEmployeesByRole(int empRoleId);
        /* Task<ICollection<EmployeeRole>> GetRoles();
        Task<EmployeeRole?> GetRole(int id);
        Task<bool> RoleExists(int id);
        Task<ICollection<Employee>> GetEmployeesByRole(int empRoleId);
        Task<bool> CreateRole(EmployeeRole empRole);

        Task<bool> Save();

        Task<bool> UpdateRole(EmployeeRole empRole);
        Task<bool> DeleteRole(EmployeeRole empRole); */
    }
}
