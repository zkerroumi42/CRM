using System.Collections.Generic;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Project;
using api.Helpers;
using api.interfaces;
using api.models;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly ApplicationDBContext _context;

        public ProjectRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<List<Project>> GetAllAsync(QueryObject query)
        {
            var projects = _context.Projects.AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.SortBy))
            {
                projects = query.IsDecending
                    ? projects.OrderByDescending(p => EF.Property<object>(p, query.SortBy))
                    : projects.OrderBy(p => EF.Property<object>(p, query.SortBy));
            }

            var skipNumber = (query.PageNumber - 1) * query.PageSize;
            return await projects.Skip(skipNumber).Take(query.PageSize).ToListAsync();
        }

        public async Task<Project> GetByIdAsync(int id)
        {
            return await _context.Projects.FindAsync(id);
        }

        public async Task<Project> CreateAsync(Project projectModel)
        {
            await _context.Projects.AddAsync(projectModel);
            await _context.SaveChangesAsync();
            return projectModel;
        }

        public async Task<Project> UpdateAsync(int id, UpdateProjectRequestDto projectDto)
        {
            var project = await _context.Projects.FindAsync(id);

            if (project == null) return null;

            project.ProjectName = projectDto.ProjectName;
            project.CreateAt = projectDto.CreateAt;
            project.StartDate = projectDto.StartDate;
            project.EndDate = projectDto.EndDate;
            project.Status = projectDto.Status;

            await _context.SaveChangesAsync();
            return project;
        }

        public async Task<Project> DeleteAsync(int id)
        {
            var project = await _context.Projects.FindAsync(id);
            if (project == null) return null;

            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();
            return project;
        }

        public async Task<List<Project>> GetByClient(int clientId)
        {
            return await _context.Projects
                                 .Where(p => p.CustomerId == clientId)
                                 .ToListAsync();
        }

        public async Task<Project> UpdateStatus(int projectId, string status)
        {
            var project = await _context.Projects.FindAsync(projectId);
            if (project == null) return null;

            project.Status = status;
            await _context.SaveChangesAsync();
            return project;
        }
    }
}
