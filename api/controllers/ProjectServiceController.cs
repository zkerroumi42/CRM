using System.Collections.Generic;
using System.Threading.Tasks;
using api.Dtos.ProjectService;
using api.Dtos.SalaryService;
using api.interfaces;
using api.Mappers;
using api.models;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectServiceController : ControllerBase
    {
        private readonly IProjectServiceRepository _projectServiceRepo;

        public ProjectServiceController(IProjectServiceRepository projectServiceRepository)
        {
            _projectServiceRepo = projectServiceRepository;
        }

        [HttpPost("assign")]
        public async Task<ActionResult<ProjectService>> AssignServiceToProject([FromBody] CreateProjectServicenRequestDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var projectService = dto.ToProjectServiceFromCreate();
            var createdService = await _projectServiceRepo.AssignServiceToProject(projectService);
            return CreatedAtAction(nameof(GetProjectServiceById), new { id = createdService.ProjectServiceId }, createdService);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ProjectService>> UpdateProjectService(int id, ProjectService projectService)
        {
            var updatedService = await _projectServiceRepo.UpdateProjectService(id, projectService);
            if (updatedService == null) return NotFound();
            return Ok(updatedService);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveServiceFromProject(int id)
        {
            var result = await _projectServiceRepo.RemoveServiceFromProject(id);
            if (!result) return NotFound();
            return NoContent();
        }

        [HttpGet("services/{projectId}")]
        public async Task<ActionResult<List<Servicee>>> GetServicesByProject(int projectId)
        {
            var services = await _projectServiceRepo.GetServicesByProject(projectId);
            return Ok(services);
        }

        [HttpGet("projects/{serviceId}")]
        public async Task<ActionResult<List<Project>>> GetProjectsByService(int serviceId)
        {
            var projects = await _projectServiceRepo.GetProjectsByService(serviceId);
            return Ok(projects);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProjectService>> GetProjectServiceById(int id)
        {
            var projectService = await _projectServiceRepo.GetProjectServiceById(id);
            if (projectService == null) return NotFound();
            return Ok(projectService);
        }
    }
}
