using FastFood.Data;
using FastFood.Dto;
using FastFood.Interfaces;
using FastFood.Models;
using Microsoft.EntityFrameworkCore;

namespace FastFood.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly DataContext _context;

        public RoleRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Role> CheckDuplicateRole(RoleDto role)
        {
            return (await GetRoles())
                .Where(r => r.Name == role.Name)
                .FirstOrDefault();
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

        public async Task<Role> GetRole(int id)
        {
            return _context.Roles.Where(r => r.Id == id).FirstOrDefault();
        }


        public async Task<ICollection<Role>> GetRoles()
        {
            return await _context.Roles.OrderBy(r => r.Name).ToListAsync();
        }

        public async Task<bool> RoleExists(int id)
        {
            return _context.Roles.Any(r => r.Id == id);
        }

        public async Task<bool> Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public async Task<bool> UpdateRole(Role role)
        {
            _context.Update(role);
            return await Save();
        }
    }

}
