using FastFood.Data;
using FastFood.Dto;
using FastFood.Interfaces;
using FastFood.Models;

namespace FastFood.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly DataContext _context;

        public EmployeeRepository(DataContext context)
        {
            _context = context;
            
        }
        public Employee CheckDuplicateEmployee(EmployeeDto employee)
        {
            return GetEmployees()
                .Where(e => e.FirstName == employee.FirstName && e.LastName == employee.LastName)
                .FirstOrDefault();
        }

        public bool CreateEmployee(Employee employee)
        {
            _context.Add(employee);
            return Save();
        }

        public bool DeleteEmployee(Employee employee)
        {
            _context.Remove(employee);
            return Save();
        }

        public bool EmployeeExists(int id)
        {
            return _context.Employees.Any(e => e.Id == id);

        }

        public Employee GetEmployee(int id)
        {
            return _context.Employees.Where(e => e.Id == id).FirstOrDefault();

        }

        public ICollection<Employee> GetEmployees()
        {
            return _context.Employees.OrderBy(e => e.LastName).ToList();

        }

        public ICollection<Employee> GetEmployeesByRestaurant(int restaurantId)
        {
            return _context.Employees.Where(e => e.RestaurantId == restaurantId).ToList();
        }

        public EmployeeLeave GetLeaveByEmployee(int employeeId)
        {
            return _context.EmployeeLeaves.Where(m => m.EmployeeId == employeeId).FirstOrDefault();

        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateEmployee(Employee employee)
        {
            _context.Update(employee);
            return Save();
        }
    }
}
