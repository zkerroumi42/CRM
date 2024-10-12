using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.models
{
    public class ProjectService
    {
        public int ProjectServiceId { get; set; }
        
        public int? CustomerId { get; set; }
        public  Customer? Customer { get; set; }
        public int? ServiceeId { get; set; }
        public  Servicee? Servicee { get; set; } 
    }
}