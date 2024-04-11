using AutoMapper;
using Employee.API.Models;
using Employee.Core.DTOs;
using Employee.Core.Entities;
using Employee.Core.Services;
using Employee.Service;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Employee.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {

        private readonly IRoleService _roleService;
        private readonly IMapper _mapper;
        public RoleController(IRoleService roleService, IMapper mapper)
        {
            _roleService = roleService;
            _mapper = mapper;
        }

        // GET: api/<RoleController>
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var roles = await _roleService.GetAllRolesAsync();
            var roleDto = _mapper.Map<IEnumerable<RoleDto>>(roles);
            return Ok(roleDto);
        }

        // GET api/<RoleController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var role = await _roleService.GetRoleByIdAsync(id);
            var roleDto = _mapper.Map<RoleDto>(role);
            return roleDto != null ? Ok(roleDto) : NotFound();
        }

        // POST api/<RoleController>
        [HttpPost]
        public async Task<ActionResult<Role>> Post([FromBody] RolePostModel role)
        {
            var roleToAdd = new Role { Name = role.Name };
            var res = await _roleService.AddRoleAsync(roleToAdd );
            return res != null ? Ok(res) : NotFound();
        }

        // PUT api/<RoleController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] RolePostModel role)
        {
            var roleToAdd = new Role { Name = role.Name };
            var res = await _roleService.UpdateRoleAsync(id, roleToAdd);
            return res != null ? Ok(res) : NotFound();
        }

        // DELETE api/<RoleController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var res = await _roleService.DeleteRoleAsync(id);
            return res != null ? Ok(res) : NotFound();
        }
    }
}
