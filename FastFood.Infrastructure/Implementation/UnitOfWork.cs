using FastFood.Domain.Interfaces;
using FastFood.Domain.RepoInterfaces;
using FastFood.Repo.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastFood.Infrastructure.Implementation
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;

        public UnitOfWork(DataContext context)
        {
            _context = context;
            Restaurant = new RestaurantRepository(_context);
            MenuItem = new MenuItemRepository(_context);
            Employee = new EmployeeRepository(_context);
            EmployeeLeave = new EmployeeLeaveRepository(_context);
            EmployeeRole = new EmployeeRoleRepository(_context);
        }
        public IRestaurantRepository Restaurant {  get; private set; }
        public IMenuItemRepository MenuItem { get; private set; }
        public IEmployeeRepository Employee { get; private set; }
        public IEmployeeLeaveRepository EmployeeLeave { get; private set; }
        public IEmployeeRoleRepository EmployeeRole { get; private set; }
        public async Task<bool> CompleteAsync()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0 ? true : false;
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
