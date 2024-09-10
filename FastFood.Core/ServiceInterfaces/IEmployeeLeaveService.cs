
using FastFood.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastFood.Domain.ServiceInterfaces
{
    public interface IEmployeeLeaveService
    {
        Task<EmployeeLeaveDto> GetLeaveByEmployee(int employeeId);
        Task<int> CalculateLeaveAccruedPerMonth(EmployeeLeaveDto employeeLeave);
        Task<int> CalculateCurrentLeave(EmployeeLeaveDto employeeLeave);

    }
}
