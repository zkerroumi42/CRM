using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.models
{
    public class Project
    {
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }=string.Empty;
        public string Status { get; set; }=string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime CreateAt { get; set; }   
        public int? CustomerId { get; set; }
        public  Customer? Customer { get; set; }
        public int? ServiceeId { get; set; }
        public  Servicee? Servicee { get; set; } 

        
    }
}