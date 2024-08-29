using FastFood.Models;

namespace FastFood.Interfaces
{
    public interface IEmployeeLeaveRepository
    {
        ICollection<EmployeeLeave> GetAllLeave();
        EmployeeLeave GetLeave(int id);
        bool CreateEmployeeLeave(EmployeeLeave employeeLeave);
        bool UpdateEmployeeLeave(EmployeeLeave employeeLeave);
        bool DeleteEmployeeLeave(EmployeeLeave employeeLeave);
        bool Save();
    }
}
