using AutoMapper;
using FastFood.Domain.Entities;
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
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public EmployeeService(
            IEmployeeRepository employeeRepository,
            IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }
        public async Task<bool> CreateEmployee(EmployeeDto employee)
        {
            if (employee == null)
                throw new Exception("Body cant be null");

            var employeeExists = await CheckDuplicateEmployee(employee);

            if (employeeExists != null)
            {
                throw new Exception("Employee already exists");
            }


            var employeeMap = _mapper.Map<Employee>(employee);


            return await _employeeRepository.CreateEmployee(employeeMap);
        }

        public async Task<bool> DeleteEmployee(int employeeId)
        {
            if (!await _employeeRepository.EmployeeExists(employeeId))
                throw new Exception("Employee not found");

            var employeeToDelete = await _employeeRepository.GetEmployee(employeeId);

            return await _employeeRepository.DeleteEmployee(employeeToDelete);
        }

        public async Task<EmployeeDto> GetEmployee(int id)
        {
            if (!await _employeeRepository.EmployeeExists(id))
                throw new Exception("Employee not found");

            var employee = _mapper.Map<EmployeeDto>(await _employeeRepository.GetEmployee(id));


            return employee;
        }

        public async Task<ICollection<EmployeeDto>> GetEmployees()
        {
            var employees = _mapper.Map<List<EmployeeDto>>(await _employeeRepository.GetEmployees());


            return employees;
        }

        public async Task<ICollection<EmployeeDto>> GetEmployeesByRestaurant(int restaurantId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateEmployee(int employeeId, EmployeeDto employee)
        {
            if (employee == null)
                throw new Exception("Body cannot be null");

            if (employeeId != employee.Id)
                throw new Exception("ID mismatch");

            if (!await _employeeRepository.EmployeeExists(employeeId))
                throw new Exception("Employee not found");

            var duplicateEmployee = await CheckDuplicateEmployee(employee);

            if (duplicateEmployee != null)
            {
                throw new Exception("Employee already exists");
            }


            var employeeMap = _mapper.Map<Employee>(employee);


            return await _employeeRepository.UpdateEmployee(employeeMap);
        }

        public async Task<EmployeeDto> CheckDuplicateEmployee(EmployeeDto employee)
        {
            return (await GetEmployees())
                .Where(e => e.FirstName == employee.FirstName && e.LastName == employee.LastName)
                .FirstOrDefault();
        }
    }
}
