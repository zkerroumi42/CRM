using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.SalaryService;
using api.interfaces;
using api.Mappers;
using api.models;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SalaryServiceController : ControllerBase
    {
        private readonly ISalaryServiceRepository _salaryServiceRepo;

        public SalaryServiceController(ISalaryServiceRepository salaryServiceRepo)
        {
            _salaryServiceRepo = salaryServiceRepo;
        }

        [HttpPost]
        public async Task<IActionResult> AssignEmployeeToService([FromBody] CreateSalaryServiceRequestDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var employeeService = dto.ToSalaryFromCreate();
            var createdService = await _salaryServiceRepo.AssignEmployeeToService(employeeService);
            return CreatedAtAction(nameof(GetEmployeeServiceById), new { id = createdService.SalaryServiceId }, createdService);
        }

        [HttpPut("{employeeServiceId:int}")]
        public async Task<IActionResult> UpdateEmployeeService([FromRoute] int employeeServiceId, [FromBody] UpdateSalaryServiceRequestDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updatedService = await _salaryServiceRepo.UpdateEmployeeService(employeeServiceId, dto.ToSalaryFromUpdate());

            if (updatedService == null)
            {
                return NotFound("Employee-Service assignment not found");
            }

            return Ok(updatedService);
        }

        [HttpDelete("{employeeServiceId:int}")]
        public async Task<IActionResult> RemoveEmployeeFromService([FromRoute] int employeeServiceId)
        {
            var removed = await _salaryServiceRepo.RemoveEmployeeFromService(employeeServiceId);
            if (!removed)
            {
                return NotFound("Employee-Service assignment not found");
            }

            return Ok("Employee removed from service successfully");
        }

        [HttpGet("employee/{employeeId:int}")]
        public async Task<IActionResult> GetServicesByEmployee([FromRoute] string employeeId)
        {
            var services = await _salaryServiceRepo.GetServicesByEmployee(employeeId);

            if (services == null || !services.Any())
            {
                return NotFound($"No services found for employee with ID {employeeId}");
            }

            return Ok(services);
        }

        [HttpGet("service/{serviceId:int}")]
        public async Task<IActionResult> GetEmployeesByService([FromRoute] int serviceId)
        {
            var employees = await _salaryServiceRepo.GetEmployeesByService(serviceId);

            if (employees == null || !employees.Any())
            {
                return NotFound($"No employees found for service with ID {serviceId}");
            }

            return Ok(employees);
        }

        [HttpGet("{employeeServiceId:int}")]
        public async Task<IActionResult> GetEmployeeServiceById([FromRoute] int employeeServiceId)
        {
            var employeeService = await _salaryServiceRepo.GetEmployeeServiceById(employeeServiceId);

            if (employeeService == null)
            {
                return NotFound("Employee-Service assignment not found");
            }

            return Ok(employeeService);
        }
    }
}
