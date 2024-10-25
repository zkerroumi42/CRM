using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Lead;

namespace api.Dtos.ProjectService
{
    public class ProjectServiceDto
    {
        public int ProjectServiceId { get; set; }
        public int? ProjectId { get; set; }
        public int? ServiceeId { get; set; }
        
    }
}