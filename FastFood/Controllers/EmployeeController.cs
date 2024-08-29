using AutoMapper;
using FastFood.Dto;
using FastFood.Interfaces;
using FastFood.Models;
using Microsoft.AspNetCore.Mvc;

namespace FastFood.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;

        public EmployeeController(
            IEmployeeRepository employeeRepository,
            IRoleRepository rolerepository,
            IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _roleRepository = rolerepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Employee>))]
        [ProducesResponseType(400)]
        public IActionResult GetEmployees()
        {
            var employees = _mapper.Map<List<EmployeeDto>>(_employeeRepository.GetEmployees());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(employees);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Employee))]
        [ProducesResponseType(400)]
        public IActionResult GetEmployee(int id) 
        {
            if (!_employeeRepository.EmployeeExists(id))
                return NotFound();

            var employee = _mapper.Map<EmployeeDto>(_employeeRepository.GetEmployee(id));
            
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(employee);
        }

        [HttpGet("{employeeId}/leave")]
        [ProducesResponseType(200, Type = typeof(EmployeeLeave))]
        [ProducesResponseType(400)]
        public IActionResult GetLeaveByEmployee(int employeeId)
        {
            if (!_employeeRepository.EmployeeExists(employeeId))
                return NotFound();
            var employeeLeave = _mapper.Map<EmployeeLeave>(_employeeRepository.GetLeaveByEmployee(employeeId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(employeeLeave);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateEmployee([FromBody] EmployeeDto createEmployee)
        {
            if (createEmployee == null)
                return BadRequest(ModelState);

            var employeeExists = _employeeRepository.CheckDuplicateEmployee(createEmployee);

            if (employeeExists != null)
            {
                ModelState.AddModelError("", "Employee already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var employeeMap = _mapper.Map<Employee>(createEmployee);

            if (!_employeeRepository.CreateEmployee(employeeMap))
            {
                ModelState.AddModelError("", "Error creating employee");
                return StatusCode(500, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok("Employee created successfully");

        }

        [HttpPut]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateEmployee(int employeeId, [FromBody] EmployeeDto updateEmployee)
        {
            if (updateEmployee == null)
                return BadRequest(ModelState);

            if (employeeId != updateEmployee.Id)
                return BadRequest(ModelState);

            if (!_employeeRepository.EmployeeExists(employeeId))
                return NotFound();

            var duplicateEmployee = _employeeRepository.CheckDuplicateEmployee(updateEmployee);

            if (duplicateEmployee != null)
            {
                ModelState.AddModelError("", "Employee already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var employeeMap = _mapper.Map<Employee>(updateEmployee);

            if (!_employeeRepository.UpdateEmployee(employeeMap))
            {
                ModelState.AddModelError("", "Error updating employee");
                return StatusCode(500, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return NoContent();

        }


        [HttpDelete]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteEmployee(int employeeId)
        {
            if (!_employeeRepository.EmployeeExists(employeeId))
                return NotFound();

            var employeeToDelete = _employeeRepository.GetEmployee(employeeId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_employeeRepository.DeleteEmployee(employeeToDelete))
            {
                ModelState.AddModelError("", "Error deleting employee");
                return StatusCode(500, ModelState);
            }

            return NoContent();

        }
    }
}
