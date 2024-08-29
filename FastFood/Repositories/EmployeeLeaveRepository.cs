using FastFood.Data;
using FastFood.Interfaces;
using FastFood.Models;
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
        public bool CreateEmployeeLeave(EmployeeLeave employeeLeave)
        {
            _context.Add(employeeLeave);
            return Save();
        }

        public bool DeleteEmployeeLeave(EmployeeLeave employeeLeave)
        {
            _context.Remove(employeeLeave);
            return Save();
        }

        public ICollection<EmployeeLeave> GetAllLeave()
        {
            return _context.EmployeeLeaves.OrderBy(el => el.Id).ToList();
        }

        public EmployeeLeave GetLeave(int id)
        {
            return _context.EmployeeLeaves.Where(el => el.Id == id).FirstOrDefault();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateEmployeeLeave(EmployeeLeave employeeLeave)
        {
            _context.Update(employeeLeave);
            return Save();
        }
    }
}
