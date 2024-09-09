
using FastFood.Domain.Entities;
using FastFood.Dto;

namespace FastFood.Domain.ServiceInterfaces
{
    public  interface IEmployeeService
    {
        Task<ICollection<EmployeeDto>> GetEmployees();
        Task<EmployeeDto> GetEmployee(int id);
        Task<ICollection<EmployeeDto>> GetEmployeesByRestaurant(int restaurantId);
        Task<bool> CreateEmployee(EmployeeDto employee);
        Task<bool> UpdateEmployee(int employeeId, EmployeeDto employee);
        Task<bool> DeleteEmployee(int employeeId);
        Task<EmployeeDto> CheckDuplicateEmployee(EmployeeDto employee);
    }
}
