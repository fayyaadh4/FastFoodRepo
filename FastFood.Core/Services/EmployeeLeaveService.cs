using FastFood.Domain.Interfaces;
using FastFood.Domain.ServiceInterfaces;
using FastFood.Dto;
using System;
using System.Collections.Generic;

namespace FastFood.Application.Services
{
    public class EmployeeLeaveService : IEmployeeLeaveService
    {
        private readonly IEmployeeLeaveRepository _employeeLeaveRepository;   
        public EmployeeLeaveService(IEmployeeLeaveRepository employeeLeaveRepository) {
            _employeeLeaveRepository = employeeLeaveRepository;
        }

        public Task<bool> CalculateCurrentLeave(EmployeeLeaveDto employeeLeave)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CalculateLeaveAccruedPerMonth(EmployeeLeaveDto employeeLeave)
        {
            throw new NotImplementedException();
        }

        public async Task<EmployeeLeaveDto> GetLeaveByEmployee(int employeeId)
        {
            var response = new EmployeeLeaveDto();

            await _employeeLeaveRepository.GetLeave(employeeId);


            return response;
        }
    }
}
