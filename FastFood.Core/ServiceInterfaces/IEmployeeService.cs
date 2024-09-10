
using FastFood.Domain.Entities;
using FastFood.Dto;

namespace FastFood.Domain.ServiceInterfaces
{
    public  interface IEmployeeService
    {
        Task<EmployeeDto> CheckDuplicateEmployee(EmployeeDto employee);
    }
}
