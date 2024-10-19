using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Project;
using api.Helpers;
using api.models;

namespace api.interfaces
{
    public interface IProjectRepository
    {
        Task<List<Project>>GetAllAsync(QueryObject query);
        Task<Project>GetByIdAsync(int id);
        Task<Project>CreateAsync(Project ProjectModel);
        Task<Project>UpdateAsync(int id,UpdateProjectRequestDto ProjectDto);
        Task<Project>DeleteAsync(int id);
        Task<List<Project>> GetByClient(int clientId);
        Task<Project> UpdateStatus(int projectId, string status);
        
    }
}