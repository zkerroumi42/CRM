using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Project;
using api.models;

namespace api.Mappers
{
    public static class ProjectMappers
    {
        public static ProjectDto ToProjectDto(this Project ProjectModel){
            return new ProjectDto{
                ProjectId=ProjectModel.ProjectId,
                ProjectName=ProjectModel.ProjectName,
                Status=ProjectModel.Status,
                StartDate=ProjectModel.StartDate,
                EndDate=ProjectModel.EndDate,
                CreateAt=ProjectModel.CreateAt,

            };
        }
        public static Project ToProjectFromCreate(this CreateProjectRequestDto ProjectDto, int customerId)
        {
            return new Project
            {
                ProjectName=ProjectDto.ProjectName,
                Status=ProjectDto.Status,
                StartDate=ProjectDto.StartDate,
                EndDate=ProjectDto.EndDate,
                CreateAt=ProjectDto.CreateAt,
            };
        }

                public static Project ToProjectFromUpdate(this UpdateProjectRequestDto ProjectDto){
            return new Project
            {
                ProjectName=ProjectDto.ProjectName,
                Status=ProjectDto.Status,
                StartDate=ProjectDto.StartDate,
                EndDate=ProjectDto.EndDate,
                CreateAt=ProjectDto.CreateAt,
            };
        }
        

        
    }
}