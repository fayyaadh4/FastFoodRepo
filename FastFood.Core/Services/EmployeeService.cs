using AutoMapper;
using FastFood.Domain.Entities;
using FastFood.Domain.Interfaces;
using FastFood.Domain.RepoInterfaces;
using FastFood.Domain.ServiceInterfaces;
using FastFood.Dto;
using System;
using System.Collections.Generic;

namespace FastFood.Application.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EmployeeService(
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<EmployeeDto> CheckDuplicateEmployee(EmployeeDto employee)
        {
            return _mapper.Map<IList<EmployeeDto>>(await _unitOfWork.Employee.GetAll())
                .Where(e => e.FirstName == employee.FirstName && e.LastName == employee.LastName)
                .FirstOrDefault();
        }
    }
}
