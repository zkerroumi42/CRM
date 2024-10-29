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
        Task<List<Project>>GetAllAsync(QO1 query);
        Task<Project?>GetByIdAsync(int id);
        Task<Project>CreateAsync(Project ProjectModel);
        Task<Project?>UpdateAsync(int id,Project ProjectModel);
        Task<Project?>DeleteAsync(int id);
        Task<List<Project>> GetByCustomerId(int customerId);
        Task<Project> UpdateStatus(int projectId, string status);
        
    }
}