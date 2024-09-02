using FastFood.Domain.Entities;

namespace FastFood.Domain.Interfaces
{
    public interface IEmployeeLeaveRepository
    {
        Task<ICollection<EmployeeLeave>> GetAllLeave();
        Task<EmployeeLeave> GetLeave(int id);
        Task<bool> CreateEmployeeLeave(EmployeeLeave employeeLeave);
        Task<bool> UpdateEmployeeLeave(EmployeeLeave employeeLeave);
        Task<bool> DeleteEmployeeLeave(EmployeeLeave employeeLeave);
        Task<bool> Save();
    }
}
