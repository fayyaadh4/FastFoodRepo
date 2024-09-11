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
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(DataContext context) : base(context)
        {
        }

        public async Task<ICollection<Employee>> GetEmployeesByRestaurant(int restaurantId)
        {
            return await _context.Employees.Where(e => e.RestaurantId == restaurantId).ToListAsync();
        }

        public async Task<EmployeeLeave?> GetLeaveByEmployee(int employeeId)
        {
            return await _context.EmployeeLeaves.Where(m => m.EmployeeId == employeeId).FirstOrDefaultAsync();

        }
    }
}
