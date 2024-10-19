using System.Collections.Generic;
using System.Threading.Tasks;
using api.Dtos.ProjectService;
using api.interfaces;
using api.models;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectServiceController : ControllerBase
    {
        private readonly IProjectServiceRepository _projectServiceRepository;

        public ProjectServiceController(IProjectServiceRepository projectServiceRepository)
        {
            _projectServiceRepository = projectServiceRepository;
        }

        [HttpPost("assign")]
        public async Task<ActionResult<ProjectService>> AssignServiceToProject(ProjectService projectService)
        {
            var result = await _projectServiceRepository.AssignServiceToProject(projectService);
            return CreatedAtAction(nameof(GetProjectServiceById), new { id = result.ProjectServiceId }, result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ProjectService>> UpdateProjectService(int id, ProjectService projectService)
        {
            var updatedService = await _projectServiceRepository.UpdateProjectService(id, projectService);
            if (updatedService == null) return NotFound();
            return Ok(updatedService);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveServiceFromProject(int id)
        {
            var result = await _projectServiceRepository.RemoveServiceFromProject(id);
            if (!result) return NotFound();
            return NoContent();
        }

        [HttpGet("services/{projectId}")]
        public async Task<ActionResult<List<Servicee>>> GetServicesByProject(int projectId)
        {
            var services = await _projectServiceRepository.GetServicesByProject(projectId);
            return Ok(services);
        }

        [HttpGet("projects/{serviceId}")]
        public async Task<ActionResult<List<Project>>> GetProjectsByService(int serviceId)
        {
            var projects = await _projectServiceRepository.GetProjectsByService(serviceId);
            return Ok(projects);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProjectService>> GetProjectServiceById(int id)
        {
            var projectService = await _projectServiceRepository.GetProjectServiceById(id);
            if (projectService == null) return NotFound();
            return Ok(projectService);
        }
    }
}
