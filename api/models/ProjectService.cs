using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.models
{
    public class ProjectService
    {
        public int ProjectServiceId { get; set; }
        
        public int? ProjectId { get; set; }
        public  Project? Project { get; set; }
        public int? ServiceeId { get; set; }
        public  Servicee? Servicee { get; set; } 
    }
}