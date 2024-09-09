using FastFood.Domain.Interfaces;
using FastFood.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using FastFood.Repo.Data;

namespace FastFood.Repo.Repositories
{
    public class RoleRepository : IEmployeeRoleRepository
    {
        private readonly DataContext _context;

        public RoleRepository(DataContext context)
        {
            _context = context;
        }


        public async Task<bool> CreateRole(EmployeeRole empRole)
        {
            _context.Add(empRole);
            return await Save();
        }

        public async Task<bool> DeleteRole(EmployeeRole empRole)
        {
            _context.Remove(empRole);
            return await Save();
        }

        public async Task<ICollection<Employee>> GetEmployeesByRole(int roleId)
        {
            return await _context.Employees.Where(e => e.RoleId == roleId).ToListAsync();

        }

        public async Task<EmployeeRole?> GetRole(int id)
        {
            return await _context.EmployeeRoles.Where(r => r.Id == id).FirstOrDefaultAsync();
        }


        public async Task<ICollection<EmployeeRole>> GetRoles()
        {
            return await _context.EmployeeRoles.OrderBy(r => r.Name).ToListAsync();
        }

        public async Task<bool> RoleExists(int id)
        {
            return await _context.EmployeeRoles.AnyAsync(r => r.Id == id);
        }

        public async Task<bool> Save()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0;
        }

        public async Task<bool> UpdateRole(EmployeeRole empRole)
        {
            _context.Update(empRole);
            return await Save();
        }
    }

}
