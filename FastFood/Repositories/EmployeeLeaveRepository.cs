using FastFood.Data;
using FastFood.Interfaces;
using FastFood.Models;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace FastFood.Repositories
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

        public async Task<EmployeeLeave> GetLeave(int id)
        {
            return _context.EmployeeLeaves.Where(el => el.Id == id).FirstOrDefault();
        }

        public async Task<bool> Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public async Task<bool> UpdateEmployeeLeave(EmployeeLeave employeeLeave)
        {
            _context.Update(employeeLeave);
            return await Save();
        }
    }
}
