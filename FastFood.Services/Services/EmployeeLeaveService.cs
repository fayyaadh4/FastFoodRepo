using FastFood.Domain.Interfaces;
using FastFood.Domain.ServiceInterfaces;
using FastFood.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastFood.Application.Services
{
    public class EmployeeLeaveService : IEmployeeLeaveService
    {
        private readonly IEmployeeLeaveRepository _employeeLeaveRepository;   
        public EmployeeLeaveService(IEmployeeLeaveRepository employeeLeaveRepository) {
            _employeeLeaveRepository = employeeLeaveRepository;
        }

        public async Task<EmployeeLeaveDto> GetLeaveByEmployee(int employeeId)
        {
            var response = new EmployeeLeaveDto();

            _employeeLeaveRepository.GetLeave(employeeId);


            return response;
        }
    }
}
