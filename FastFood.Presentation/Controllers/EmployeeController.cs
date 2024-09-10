
using FastFood.Core.Services.EmployeeCQRS.Commands;
using FastFood.Core.Services.EmployeeCQRS.Queries;
using FastFood.Domain.ServiceInterfaces;
using FastFood.Dto;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FastFood.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly IEmployeeLeaveService _employeeLeaveService;
        private readonly IMediator _mediator;

        public EmployeeController(IEmployeeService employeeService,
            IEmployeeLeaveService employeeLeaveService,
            IMediator mediator)
        {
            _employeeService = employeeService;
            _employeeLeaveService = employeeLeaveService;
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<EmployeeDto>))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetEmployees()
        {
            var query = new GetEmployeesQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(EmployeeDto))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetEmployee(int id) 
        {
            var query = new GetEmployeeQuery(id);
            var result = await _mediator.Send(query);
            
            return Ok(result);
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
            var command = new CreateEmployeeCommand(createEmployee);
            var result = await _mediator.Send(command);

            return Ok("Employee created successfully");

        }

        [HttpPut]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateEmployee(int employeeId, [FromBody] EmployeeDto updateEmployee)
        {
            var command = new UpdateEmployeeCommand(employeeId, updateEmployee);
            var result = await _mediator.Send(command);
            return NoContent();

        }


        [HttpDelete]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteEmployee(int employeeId)
        {
            var command = new DeleteEmployeeCommand(employeeId);
            var result = await _mediator.Send(command);
            return NoContent();

        }
    }
}
