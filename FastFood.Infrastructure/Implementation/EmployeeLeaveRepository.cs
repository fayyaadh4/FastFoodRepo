using FastFood.Domain.Entities;
using FastFood.Domain.Interfaces;
using FastFood.Repo.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastFood.Infrastructure.Implementation
{
    public class EmployeeLeaveRepository : GenericRepository<EmployeeLeave>, IEmployeeLeaveRepository
    {
        public EmployeeLeaveRepository(DataContext context) : base(context)
        {
            
        }
    }
}
