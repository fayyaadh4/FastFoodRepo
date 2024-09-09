using FastFood.Domain.Interfaces;
using FastFood.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using FastFood.Repo.Data;

namespace FastFood.Repo.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly DataContext _context;

        public RoleRepository(DataContext context)
        {
            _context = context;
        }


        public async Task<bool> CreateRole(Role role)
        {
            _context.Add(role);
            return await Save();
        }

        public async Task<bool> DeleteRole(Role role)
        {
            _context.Remove(role);
            return await Save();
        }

        public async Task<ICollection<Employee>> GetEmployeesByRole(int roleId)
        {
            return await _context.Employees.Where(e => e.RoleId == roleId).ToListAsync();

        }

        public async Task<Role?> GetRole(int id)
        {
            return await _context.Roles.Where(r => r.Id == id).FirstOrDefaultAsync();
        }


        public async Task<ICollection<Role>> GetRoles()
        {
            return await _context.Roles.OrderBy(r => r.Name).ToListAsync();
        }

        public async Task<bool> RoleExists(int id)
        {
            return await _context.Roles.AnyAsync(r => r.Id == id);
        }

        public async Task<bool> Save()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0;
        }

        public async Task<bool> UpdateRole(Role role)
        {
            _context.Update(role);
            return await Save();
        }
    }

}
