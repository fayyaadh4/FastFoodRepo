using FastFood.Dto;
using FastFood.Models;

namespace FastFood.Interfaces
{
    public interface IEmployeeRepository
    {
        ICollection<Employee> GetEmployees();
        Employee GetEmployee(int id);
        bool EmployeeExists(int id);
        EmployeeLeave GetLeaveByEmployee(int employeeId);
        ICollection<Employee> GetEmployeesByRestaurant(int restaurantId);
        bool CreateEmployee(Employee employee);

        bool Save();

        bool UpdateEmployee(Employee employee);
        bool DeleteEmployee(Employee employee);
        Employee CheckDuplicateEmployee(EmployeeDto employee);
    }
}
