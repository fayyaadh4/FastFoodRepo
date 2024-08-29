using FastFood.Data;
using FastFood.Dto;
using FastFood.Interfaces;
using FastFood.Models;
using Microsoft.EntityFrameworkCore;

namespace FastFood.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly DataContext _context;

        public EmployeeRepository(DataContext context)
        {
            _context = context;
            
        }
        public async Task<Employee> CheckDuplicateEmployee(EmployeeDto employee)
        {
            return (await GetEmployees())
                .Where(e => e.FirstName == employee.FirstName && e.LastName == employee.LastName)
                .FirstOrDefault();
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
            return _context.Employees.Any(e => e.Id == id);

        }

        public async Task<Employee> GetEmployee(int id)
        {
            return _context.Employees.Where(e => e.Id == id).FirstOrDefault();

        }

        public async Task<ICollection<Employee>> GetEmployees()
        {
            return await _context.Employees.OrderBy(e => e.LastName).ToListAsync();

        }

        public async Task<ICollection<Employee>> GetEmployeesByRestaurant(int restaurantId)
        {
            return await _context.Employees.Where(e => e.RestaurantId == restaurantId).ToListAsync();
        }

        public async Task<EmployeeLeave> GetLeaveByEmployee(int employeeId)
        {
            return _context.EmployeeLeaves.Where(m => m.EmployeeId == employeeId).FirstOrDefault();

        }

        public async Task<bool> Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public async Task<bool> UpdateEmployee(Employee employee)
        {
            _context.Update(employee);
            return await Save();
        }
    }
}
