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

        public Role CheckDuplicateRole(RoleDto role)
        {
            return GetRoles()
                .Where(r => r.Name == role.Name)
                .FirstOrDefault();
        }

        public bool CreateRole(Role role)
        {
            _context.Add(role);
            return Save();
        }

        public bool DeleteRole(Role role)
        {
            _context.Remove(role);
            return Save();
        }

        public ICollection<Employee> GetEmployeesByRole(int roleId)
        {
            return _context.Employees.Where(e => e.RoleId == roleId).ToList();

        }

        public Role GetRole(int id)
        {
            return _context.Roles.Where(r => r.Id == id).FirstOrDefault();
        }

        public ICollection<Role> GetRoles()
        {
            return _context.Roles.OrderBy(r => r.Name).ToList();
        }

        public bool RoleExists(int id)
        {
            return _context.Roles.Any(r => r.Id == id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateRole(Role role)
        {
            _context.Update(role);
            return Save();
        }
    }

}
