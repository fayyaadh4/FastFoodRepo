
using FastFood.Core.Services.EmployeeCQRS.Commands;
using FastFood.Core.Services.EmployeeRoleCQRS.Commands;
using FastFood.Core.Services.EmployeeRoleCQRS.Queries;
using FastFood.Domain.ServiceInterfaces;
using FastFood.Dto;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FastFood.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : Controller
    {
        private readonly IRoleService _roleService;
        private readonly IMediator _mediator;

        public RoleController(IRoleService roleService,
            IMediator mediator)
        {
            _roleService = roleService;
            _mediator = mediator;
        }


        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(EmployeeRoleDto))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetRole(int id)
        {
            var query = new GetRoleQuery(id);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<EmployeeRoleDto>))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetRoles()
        {
            var query = new GetRolesQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("employees/{roleId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<EmployeeDto>))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetEmployeesByRole(int roleId)
        {
            var query = new GetEmployeesByRoleQuery(roleId);
            var result = await _mediator.Send(query);
            
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateRole([FromBody] EmployeeRoleDto createRole)
        {
            var command = new CreateRoleCommand(createRole);
            var result = await _mediator.Send(command);
            return Ok("Role successfully created");
        }

        [HttpPut("{roleId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateRole(int roleId,
             [FromBody] EmployeeRoleDto updateRole)
        {
            var command = new UpdateRoleCommand(roleId, updateRole);
            var result = await _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{roleId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteRole(int roleId)
        {
            var command = new DeleteRoleCommand(roleId);
            var result = await _mediator.Send(command);
            return NoContent();


        }
    }
}
