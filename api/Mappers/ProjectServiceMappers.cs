using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.ProjectService;
using api.models;

namespace api.Mappers
{
    public static class ProjectServiceMappers
    {
        public static ProjectServiceDto ToProjectServiceDto(this ProjectService ProjectServiceModel){
            return new ProjectServiceDto{
                ProjectServiceId=ProjectServiceModel.ProjectServiceId,
            };
        }
        public static ProjectService ToProjectServiceFromCreate(this CreateProjectServicenRequestDto ProjectServiceDto){
            return new ProjectService
            {
                ProjectId=ProjectServiceDto.ProjectId,
                ServiceeId=ProjectServiceDto.ServiceeId,
                
            };
        }

        public static ProjectService ToProjectServiceFromUpdate(this UpdateProjectServiceRequestDto ProjectServiceDto){
            return new ProjectService
            {
            };
        }
        

        
    }
}