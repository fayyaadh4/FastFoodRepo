using FastFood.Domain.Interfaces;
using FastFood.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Data;
using FastFood.Repo.Data;

namespace FastFood.Repo.Repositories
{
    public class EmployeeLeaveRepository : IEmployeeLeaveRepository
    {
        private readonly DataContext _context;

        public EmployeeLeaveRepository(DataContext context)
        {
            _context = context;   
        }
        public async Task<bool> CreateEmployeeLeave(EmployeeLeave employeeLeave)
        {
            _context.Add(employeeLeave);
            return await Save();
        }

        public async Task<bool> DeleteEmployeeLeave(EmployeeLeave employeeLeave)
        {
            _context.Remove(employeeLeave);
            return await Save();
        }

        public async Task<ICollection<EmployeeLeave>> GetAllLeave()
        {
            return await _context.EmployeeLeaves.OrderBy(el => el.Id).ToListAsync();
        }

        public async Task<EmployeeLeave?> GetLeave(int id)
        {
            return await _context.EmployeeLeaves.Where(el => el.Id == id).FirstOrDefaultAsync();
        }

        public async Task<bool> Save()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0;
        }

        public async Task<bool> UpdateEmployeeLeave(EmployeeLeave employeeLeave)
        {
            _context.Update(employeeLeave);
            return await Save();
        }
    }
}
