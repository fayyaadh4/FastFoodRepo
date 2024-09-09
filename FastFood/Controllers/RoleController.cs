
using FastFood.Domain.ServiceInterfaces;
using FastFood.Dto;
using Microsoft.AspNetCore.Mvc;

namespace FastFood.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : Controller
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }


        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(EmployeeRoleDto))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetRole(int id)
        {
            var role = await _roleService.GetRole(id);


            return Ok(role);
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<EmployeeRoleDto>))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetRoles()
        {
            var roles = await _roleService.GetRoles();

            return Ok(roles);
        }

        [HttpGet("empployees/{roleId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<EmployeeDto>))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetEmployeesByRole(int roleId)
        {
            var employees = await _roleService.GetEmployeesByRole(roleId);

            return Ok(employees);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateRole([FromBody] EmployeeRoleDto createRole)
        {
            await _roleService.CreateRole(createRole);
            return Ok("Role successfully created");
        }

        [HttpPut("{roleId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateRole(int roleId,
             [FromBody] EmployeeRoleDto updateRole)
        {
            await _roleService.UpdateRole(roleId, updateRole);
            return NoContent();
        }

        [HttpDelete("{roleId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteRole(int roleId)
        {
            await _roleService.DeleteRole(roleId);
            return NoContent();


        }
    }
}
