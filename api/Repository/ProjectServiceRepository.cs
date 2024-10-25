using System.Collections.Generic;
using System.Threading.Tasks;
using api.Data;
using api.models;
using api.interfaces;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories
{
    public class ProjectServiceRepository : IProjectServiceRepository
    {
        private readonly ApplicationDBContext _context;

        public ProjectServiceRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<ProjectService> AssignServiceToProject(ProjectService projectService)
        {
            _ = await _context.ProjectServices.AddAsync(projectService);
            _ = await _context.SaveChangesAsync();
            return projectService;
        }

        public async Task<ProjectService> UpdateProjectService(int projectServiceId, ProjectService projectService)
        {
            var existingProjectService = await _context.ProjectServices.FindAsync(projectServiceId);
            if (existingProjectService == null) return null;

            existingProjectService.ProjectId = projectService.ProjectId;
            existingProjectService.ServiceeId = projectService.ServiceeId;

            _ = await _context.SaveChangesAsync();
            return existingProjectService;
        }

        public async Task<bool> RemoveServiceFromProject(int projectServiceId)
        {
            var projectService = await _context.ProjectServices.FindAsync(projectServiceId);
            if (projectService == null) return false;

            _ = _context.ProjectServices.Remove(projectService);
            _ = await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Servicee>> GetServicesByProject(int projectId)
        {
            return await _context.ProjectServices
                                 .Where(ps => ps.ProjectId == projectId)
                                 .Select(ps => ps.Servicee)
                                 .ToListAsync();
        }

        public async Task<List<Project>> GetProjectsByService(int serviceId)
        {

            return await _context.ProjectServices
                                 .Where(ps => ps.ServiceeId == serviceId)
                                 .Select(ps => ps.Project)
                                 .ToListAsync();

        }

        public async Task<ProjectService> GetProjectServiceById(int projectServiceId)
        {
            return await _context.ProjectServices.FindAsync(projectServiceId);
        }
    }
}
