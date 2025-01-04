using EmployeeAdminPortal.Models.DTOs;
using EmployeeAdminPortal.Models.Responses;
using EmployeeAdminPortal.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace EmployeeAdminPortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeesController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEmployees([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            if (pageNumber <= 0)
            {
                return BadRequest(new { message = "Page number must be greater than zero." });
            }

            if (pageSize <= 0)
            {
                return BadRequest(new { message = "Page size must be greater than zero." });
            }

            var pagedEmployees = await _employeeService.GetAllEmployeesAsync(pageNumber, pageSize);
            return Ok(pagedEmployees);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetEmployeeById(Guid id)
        {
            var employee = await _employeeService.GetEmployeeByIdAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            return Ok(employee);
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployee(AddEmployeeDto addEmployeeDto)
        {
            try
            {
                var employee = await _employeeService.AddEmployeeAsync(addEmployeeDto);
                return CreatedAtAction(nameof(GetEmployeeById), new { id = employee.Id }, employee);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateEmployee(Guid id, UpdateEmployeeDto updateEmployeeDto)
        {
            try
            {
                var updatedEmployee = await _employeeService.UpdateEmployeeAsync(id, updateEmployeeDto);
                if (updatedEmployee == null)
                {
                    return NotFound();
                }
                return Ok(updatedEmployee);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteEmployee(Guid id)
        {
            var result = await _employeeService.DeleteEmployeeAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
