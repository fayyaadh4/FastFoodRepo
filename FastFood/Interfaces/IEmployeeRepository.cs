using FastFood.Dto;
using FastFood.Models;

namespace FastFood.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<ICollection<Employee>> GetEmployees();
        Task<Employee> GetEmployee(int id);
        Task<bool> EmployeeExists(int id);
        Task<EmployeeLeave> GetLeaveByEmployee(int employeeId);
        Task<ICollection<Employee>> GetEmployeesByRestaurant(int restaurantId);
        Task<bool> CreateEmployee(Employee employee);

        Task<bool> Save();

        Task<bool> UpdateEmployee(Employee employee);
        Task<bool> DeleteEmployee(Employee employee);
        Task<Employee> CheckDuplicateEmployee(EmployeeDto employee);
    }
}
