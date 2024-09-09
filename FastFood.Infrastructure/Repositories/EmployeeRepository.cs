
using FastFood.Domain.Interfaces;
using FastFood.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using FastFood.Repo.Data;

namespace FastFood.Repo.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly DataContext _context;

        public EmployeeRepository(DataContext context)
        {
            _context = context;
            
        }

        public async Task<bool> CreateEmployee(Employee employee)
        {
            _context.Add(employee);
            return await Save();
        }

        public async Task<bool> DeleteEmployee(Employee employee)
        {
            _context.Remove(employee);
            return await Save();
        }

        public async Task<bool> EmployeeExists(int id)
        {
            return await _context.Employees.AnyAsync(e => e.Id == id);

        }

        public async Task<Employee?> GetEmployee(int id)
        {
            return await _context.Employees.Where(e => e.Id == id).FirstOrDefaultAsync();

        }

        public async Task<ICollection<Employee>> GetEmployees()
        {
            return await _context.Employees.OrderBy(e => e.LastName).ToListAsync();

        }

        public async Task<ICollection<Employee>> GetEmployeesByRestaurant(int restaurantId)
        {
            return await _context.Employees.Where(e => e.RestaurantId == restaurantId).ToListAsync();
        }

        public async Task<EmployeeLeave?> GetLeaveByEmployee(int employeeId)
        {
            return await _context.EmployeeLeaves.Where(m => m.EmployeeId == employeeId).FirstOrDefaultAsync();

        }

        public async Task<bool> Save()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0 ? true : false;
        }

        public async Task<bool> UpdateEmployee(Employee employee)
        {
            _context.Update(employee);
            return await Save();
        }
    }
}
