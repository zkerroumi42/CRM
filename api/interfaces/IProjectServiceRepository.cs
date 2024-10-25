using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.models;

namespace api.interfaces
{
    public interface IProjectServiceRepository
    {
        Task<ProjectService> AssignServiceToProject(ProjectService projectService);
        Task<ProjectService> UpdateProjectService(int projectServiceId, ProjectService projectService);
        Task<bool> RemoveServiceFromProject(int projectServiceId);
        Task<List<Servicee>> GetServicesByProject(int projectId);
        Task<List<Project>> GetProjectsByService(int serviceId);
        Task<ProjectService> GetProjectServiceById(int projectServiceId); 
    }
}