using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Project;
using api.Helpers;
using api.interfaces;
using api.Mappers;
using api.models;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectRepository _projectRepo;

        public ProjectController(IProjectRepository projectRepo)
        {
            _projectRepo = projectRepo;
        }

        // Get all projects with query filtering
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] QueryObject query)
        {
            var projects = await _projectRepo.GetAllAsync(query);
            if (projects == null || !projects.Any())
            {
                return NotFound("No projects found.");
            }

            var projectDtos = projects.Select(p => p.ToProjectDto());
            return Ok(projectDtos);
        }

        // Get a project by ID
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var project = await _projectRepo.GetByIdAsync(id);
            if (project == null)
            {
                return NotFound($"Project with ID {id} not found.");
            }

            return Ok(project.ToProjectDto());
        }

        // Create a new project
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProjectRequestDto projectDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var projectModel = projectDto.ToProjectFromCreate();
            var createdProject = await _projectRepo.CreateAsync(projectModel);
            return CreatedAtAction(nameof(GetById), new { id = createdProject.ProjectId }, createdProject.ToProjectDto());
        }

        // Update an existing project
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateProjectRequestDto projectDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updatedProject = await _projectRepo.UpdateAsync(id, projectDto);
            if (updatedProject == null)
            {
                return NotFound($"Project with ID {id} not found.");
            }

            return Ok(updatedProject.ToProjectDto());
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var deletedProject = await _projectRepo.DeleteAsync(id);
            if (deletedProject == null)
            {
                return NotFound($"Project with ID {id} not found.");
            }

            return Ok(deletedProject.ToProjectDto());
        }

        [HttpGet("client/{clientId:int}")]
        public async Task<IActionResult> GetByClient([FromRoute] int clientId)
        {
            var projects = await _projectRepo.GetByClient(clientId);
            if (projects == null || !projects.Any())
            {
                return NotFound($"No projects found for client with ID {clientId}.");
            }

            return Ok(projects.Select(p => p.ToProjectDto()));
        }

        // Update the status of a project
        [HttpPatch("{id:int}/status")]
        public async Task<IActionResult> UpdateStatus([FromRoute] int id, [FromBody] string status)
        {
            var updatedProject = await _projectRepo.UpdateStatus(id, status);
            if (updatedProject == null)
            {
                return NotFound($"Project with ID {id} not found.");
            }

            return Ok(updatedProject.ToProjectDto());
        }
    }
}
