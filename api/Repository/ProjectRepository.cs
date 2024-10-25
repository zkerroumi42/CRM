using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Helpers;
using api.interfaces;
using api.models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class ProjectRepository:IProjectRepository
    {
        private readonly ApplicationDBContext _context;
        public ProjectRepository(ApplicationDBContext context)
        {
            _context=context;
            
        }

        public async Task<Project> CreateAsync(Project ProjectModel)
        {
            _ = await _context.Projects.AddAsync(ProjectModel);
            _ = await _context.SaveChangesAsync();
            return ProjectModel;
        }

        public async Task<Project?> DeleteAsync(int id)
        {
            var ProjectModel=await _context.Projects.FirstOrDefaultAsync(x=>x.ProjectId==id);

            if (ProjectModel==null)
            {
                return null;
            }
            _ = _context.Projects.Remove(ProjectModel);
            _ = await _context.SaveChangesAsync();
            return ProjectModel;
        }

        public async Task<List<Project>> GetAllAsync(QueryObject query)
        {
            var Projects = _context.Projects.AsQueryable();
            if (!string.IsNullOrWhiteSpace(query.SortBy))
            {
                Projects = query.IsDecending
                    ? Projects.OrderByDescending(c => EF.Property<object>(c, query.SortBy))
                    : Projects.OrderBy(c => EF.Property<object>(c, query.SortBy));
            }

            var skipNumber = (query.PageNumber - 1) * query.PageSize;
            return await Projects.Skip(skipNumber).Take(query.PageSize).ToListAsync();
                
                }

        public async Task<List<Project>> GetByCustomerId(int customerId)
        {
                return await _context.Projects
                         .Where(c => c.CustomerId == customerId) 
                         .ToListAsync();
        }

        public async Task<Project?> GetByIdAsync(int id)
        {
             return await _context.Projects.FindAsync(id);
        }

        public async Task<Project?> UpdateAsync(int id, Project ProjectModel)
        {
            var existingProject=await _context.Projects.FindAsync(id);
            if (existingProject==null)
            {
                return null;
            }
            existingProject.ProjectName=ProjectModel.ProjectName;
            existingProject.Status=ProjectModel.Status;
            existingProject.CreateAt=ProjectModel.CreateAt;
            existingProject.StartDate=ProjectModel.StartDate;
            existingProject.EndDate=ProjectModel.EndDate;
            _ = await _context.SaveChangesAsync();
            return existingProject;

        }

        public async Task<Project> UpdateStatus(int projectId, string status)
        {
            var project = await _context.Projects.FindAsync(projectId);
            if (project == null) return null;
            project.Status = status;
            _ = await _context.SaveChangesAsync();
            return  project;
        }
        
    }
}