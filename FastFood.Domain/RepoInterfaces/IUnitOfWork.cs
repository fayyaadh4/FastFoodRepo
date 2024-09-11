using FastFood.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastFood.Domain.RepoInterfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRestaurantRepository Restaurant { get; }
        IMenuItemRepository MenuItem { get; }
        IEmployeeRepository Employee { get; }
        IEmployeeLeaveRepository EmployeeLeave { get; }
        IEmployeeRoleRepository EmployeeRole { get; }
        Task<bool> CompleteAsync();
    }
}
