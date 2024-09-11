using FastFood.Domain.Entities;
using FastFood.Domain.Interfaces;
using FastFood.Repo.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastFood.Infrastructure.Implementation
{
    public class EmployeeRoleRepository : GenericRepository<EmployeeRole>, IEmployeeRoleRepository
    {
        public EmployeeRoleRepository(DataContext context) : base(context)
        {
        }

        public async Task<ICollection<Employee>> GetEmployeesByRole(int empRoleId)
        {
            return await _context.Employees.Where(e => e.RoleId == empRoleId).ToListAsync();
        }
    }
}
