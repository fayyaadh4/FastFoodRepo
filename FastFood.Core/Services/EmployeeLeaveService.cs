using FastFood.Domain.Interfaces;
using FastFood.Domain.RepoInterfaces;
using FastFood.Domain.ServiceInterfaces;
using FastFood.Dto;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace FastFood.Application.Services
{
    public class EmployeeLeaveService : IEmployeeLeaveService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;

        public EmployeeLeaveService(IUnitOfWork unitOfWork,
            IConfiguration configuration) 
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
        }

        public async Task<int> CalculateCurrentLeave(EmployeeLeaveDto employeeLeave)
        {
            return  employeeLeave.TotalLeave - employeeLeave.LeaveTaken;
        }

        public async Task<int> CalculateLeaveAccruedPerMonth(EmployeeLeaveDto employeeLeave)
        {
            // 1 extra leave day for every 5 yers of service
            var yearsAtCompany = employeeLeave.YearsAtCompany;
            int longServiceAddedLeave = yearsAtCompany / 5;
            // store variable in app settings.json and fetch it to be used
            return Int32.Parse(_configuration["Leave:AnnualLeave"]) + longServiceAddedLeave;
        }

        public async Task<EmployeeLeaveDto> GetLeaveByEmployee(int employeeId)
        {
            var response = new EmployeeLeaveDto();

            await _unitOfWork.EmployeeLeave.GetById(employeeId);


            return response;
        }
    }
}
