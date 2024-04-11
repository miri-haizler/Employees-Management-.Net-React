using AutoMapper;
using Employee.API.Models;
using Employee.Core;
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
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly IMapper _mapper;
        public EmployeeController(IEmployeeService employeeService, IMapper mapping)
        {
            _employeeService = employeeService;
            _mapper = mapping;
        }

        // GET: api/<EmployeeController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> Get()
        {
            var employees = await _employeeService.GetAllEmployeesAsync();
            var employeesDto = _mapper.Map<IEnumerable<EmployeeDto>>(employees);
            return Ok(employeesDto);
        }

        [HttpGet("{tz}")]
        public async Task<IActionResult> Get(string tz)
        {
            var employee = await _employeeService.GetEmployeeByIdAsync(tz);
            return employee != null ? Ok(employee) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] EmployeePostModel employee)
        {
            var employeeToAdd = new EmployeeDetails
            {
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Tz = employee.Tz,
                DateOfBirth = employee.DateOfBirth,
                Gender = employee.Gender,
                IsActive = employee.IsActive,
                StartDate = employee.StartDate,
                RolesEmployee = new List<RoleEmployee>()
            };
            employee.RolesEmployee.ToList().ForEach(re =>
            employeeToAdd.RolesEmployee.Add(new RoleEmployee
            {
                RoleId = re.RoleId,
                IsActive = re.IsActive,
                IsManagerial = re.IsManagerial,
                StartDateOfJob = re.StartDateOfJob
            }));
            var isSuccess = await _employeeService.AddEmployeeAsync(employeeToAdd);
            return isSuccess ? Ok(employee) : BadRequest();
        }

        // PUT api/<EmployeeController>/5
        [HttpPut("{tz}")]
        public async Task<IActionResult> Put(string tz, [FromBody] EmployeePostModel employee)
        {
            var employeeToAdd = new EmployeeDetails
            {
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Tz = employee.Tz,
                DateOfBirth = employee.DateOfBirth,
                StartDate = employee.StartDate,
                Gender = employee.Gender,
                IsActive = employee.IsActive

            };
            employeeToAdd.RolesEmployee = new List<RoleEmployee>();
            employee.RolesEmployee.ToList().ForEach(re =>
            employeeToAdd.RolesEmployee.Add(new RoleEmployee
            {
                Id = re.Id,
                RoleId = re.RoleId,
                IsActive = re.IsActive,
                IsManagerial = re.IsManagerial,
                StartDateOfJob = re.StartDateOfJob
            }));
            var emp = await _employeeService.UpdateEmployeeAsync(tz, employeeToAdd);
            return emp != null ? Ok(emp) : NotFound();
        }

        // DELETE api/<EmployeeController>/5
        [HttpDelete("{tz}")]
        public async Task<ActionResult> DeleteAsync(string tz)
        {
            var emp = await _employeeService.DeleteEmployeeAsync(tz);
            return emp != null ? Ok(emp) : NotFound();
        }
    }
}
