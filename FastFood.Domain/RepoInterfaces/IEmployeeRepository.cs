
using FastFood.Domain.Entities;
using FastFood.Domain.RepoInterfaces;

namespace FastFood.Domain.Interfaces
{
    public interface IEmployeeRepository : IGenericRepository<Employee>
    {
        Task<EmployeeLeave?> GetLeaveByEmployee(int employeeId);
        Task<ICollection<Employee>> GetEmployeesByRestaurant(int restaurantId);
        /*Task<ICollection<Employee>> GetEmployees();
        Task<Employee?> GetEmployee(int id);
        Task<bool> EmployeeExists(int id);
        Task<EmployeeLeave?> GetLeaveByEmployee(int employeeId);
        Task<ICollection<Employee>> GetEmployeesByRestaurant(int restaurantId);
        Task<bool> CreateEmployee(Employee employee);

        Task<bool> Save();

        Task<bool> UpdateEmployee(Employee employee);
        Task<bool> DeleteEmployee(Employee employee);*/
    }
}
