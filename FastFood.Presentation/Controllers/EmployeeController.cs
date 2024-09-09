
using FastFood.Domain.ServiceInterfaces;
using FastFood.Dto;
using Microsoft.AspNetCore.Mvc;

namespace FastFood.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly IEmployeeLeaveService _employeeLeaveService;

        public EmployeeController(IEmployeeService employeeService,
            IEmployeeLeaveService employeeLeaveService)
        {
            _employeeService = employeeService;
            _employeeLeaveService = employeeLeaveService;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<EmployeeDto>))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetEmployees()
        {
            var employees = await _employeeService.GetEmployees();

            return Ok(employees);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(EmployeeDto))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetEmployee(int id) 
        {
            var employee = await _employeeService.GetEmployee(id);
            

            return Ok(employee);
        }

        [HttpGet("{employeeId}/leave")]
        [ProducesResponseType(200, Type = typeof(EmployeeLeaveDto))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetLeaveByEmployee(int employeeId)
        {
            var employeeLeave = await _employeeLeaveService.GetLeaveByEmployee(employeeId);

            return Ok(employeeLeave);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateEmployee([FromBody] EmployeeDto createEmployee)
        {
            await _employeeService.CreateEmployee(createEmployee);

            return Ok("Employee created successfully");

        }

        [HttpPut]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateEmployee(int employeeId, [FromBody] EmployeeDto updateEmployee)
        {
            await _employeeService.UpdateEmployee(employeeId, updateEmployee);
            return NoContent();

        }


        [HttpDelete]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteEmployee(int employeeId)
        {
            await _employeeService.DeleteEmployee(employeeId);
            return NoContent();

        }
    }
}
