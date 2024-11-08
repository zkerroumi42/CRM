using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Project;
using api.Helpers;
using api.interfaces;
using api.Mappers;
using api.models;
using api.Repository;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectRepository _ProjectRepo;
        private readonly ICustomerRepository _CustomerRepo;

        public ProjectController(IProjectRepository ProjectRepo, ICustomerRepository CustomerRepo)
        {
            _ProjectRepo = ProjectRepo;
            _CustomerRepo = CustomerRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] QO1 query)
        {
            var Activities = await _ProjectRepo.GetAllAsync(query);
            var ProjectDtos = Activities.Select(s => s.ToProjectDto());
            return Ok(ProjectDtos);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var Project = await _ProjectRepo.GetByIdAsync(id);
            if (Project == null)
            {
                return NotFound("Project not found");
            }

            return Ok(Project.ToProjectDto());
        }

        [HttpPost("{CustomerId:int}")]
        public async Task<IActionResult> Create([FromRoute] int CustomerId, [FromBody] CreateProjectRequestDto ProjectDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!await _CustomerRepo.CustomerExists(CustomerId))
            {
                return BadRequest("Customer does not exist");
            }

            var ProjectModel = ProjectDto.ToProjectFromCreate(CustomerId);
            _ = await _ProjectRepo.CreateAsync(ProjectModel);
            return CreatedAtAction(nameof(GetById), new { id = ProjectModel.ProjectId }, ProjectModel.ToProjectDto());
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateProjectRequestDto updateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updatedProject = await _ProjectRepo.UpdateAsync(id, updateDto.ToProjectFromUpdate());

            if (updatedProject == null)
            {
                return NotFound("Project not found");
            }

            return Ok(updatedProject.ToProjectDto());
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var Project = await _ProjectRepo.DeleteAsync(id);

            if (Project == null)
            {
                return NotFound("Project does not exist");
            }

            return Ok(Project);
        }
        //
        [HttpGet("{customerId}")]
        public async Task<ActionResult<List<Project>>> GetByCustomerId(int customerId)
        {
            var Activities = await _ProjectRepo.GetByCustomerId(customerId);

            if (Activities == null || Activities.Count == 0)
            {
                return NotFound($"No Activities found for customer with ID {customerId}.");
            }

            return Ok(Activities);
        }
        [HttpPatch("{id}/status")]
        public async Task<ActionResult<Project>> UpdateStatus(int id, [FromBody] UpdateProjectRequestDto statusDto){
            var updatedProject = await _ProjectRepo.UpdateStatus(id, statusDto.Status);
            if (updatedProject == null) return NotFound();
            return Ok(updatedProject);

        }

    }
}
