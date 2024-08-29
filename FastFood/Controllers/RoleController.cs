using AutoMapper;
using FastFood.Dto;
using FastFood.Interfaces;
using FastFood.Models;
using FastFood.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace FastFood.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : Controller
    {
        private readonly RoleRepository _roleRepository;
        private readonly IMapper _mapper;

        public RoleController(
            RoleRepository roleRepository,
            IMapper mapper
            )
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
        }


        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Role))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetRestaurant(int id)
        {
            if (!await _roleRepository.RoleExists(id))
                return NotFound("Role does not exist");

            var role = _mapper.Map<RoleDto>(await _roleRepository.GetRole(id));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(role);
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Role>))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetRestaurants()
        {
            var roles = _mapper.Map<List<RoleDto>>(await _roleRepository.GetRoles());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(roles);
        }

        [HttpGet("empployees/{roleId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Employee>))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetEmployeesByRole(int roleId)
        {
            if (!await _roleRepository.RoleExists(roleId))
                return NotFound("Role does not exist");

            var employees = _mapper.Map<List<EmployeeDto>>(await _roleRepository.GetEmployeesByRole(roleId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(employees);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateRole([FromBody] RoleDto createRole)
        {
            if (createRole == null)
                return BadRequest(ModelState);

            var roleExists = await _roleRepository.CheckDuplicateRole(createRole);

            if (roleExists != null)
            {
                ModelState.AddModelError("", "Role already exists");
                return StatusCode(422, ModelState);
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var roleMap = _mapper.Map<Role>(createRole);

            if (!await _roleRepository.CreateRole(roleMap))
            {
                ModelState.AddModelError("", "Issue creating role");
                return StatusCode(422, ModelState);
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok("Role successfully created");
        }

        [HttpPut("{roleId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateRole(int roleId,
             [FromBody] RoleDto updateRole)
        {
            if (updateRole == null)
                return BadRequest(ModelState);

            if (roleId != updateRole.Id)
                return BadRequest(ModelState);

            if (!await _roleRepository.RoleExists(roleId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var roleMap = _mapper.Map<Role>(updateRole);

            if (!await _roleRepository.UpdateRole(roleMap))
            {
                ModelState.AddModelError("", "Something went wrong while updating the role");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{roleId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteRole(int roleId)
        {
            if (!await _roleRepository.RoleExists(roleId))
                return NotFound();

            var roleToDelete = await _roleRepository.GetRole(roleId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!await _roleRepository.DeleteRole(roleToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting role");
                return StatusCode(500, ModelState);
            }

            return NoContent();


        }
    }
}
