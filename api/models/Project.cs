using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using api.Helpers;

namespace api.models
{
        [Table("Projects")]

    public class Project
    {
        [Key]
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }=string.Empty;
        public string Status { get; set; }=ProjectStatus.NotStarted;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime CreateAt { get; set; }   
        public int? CustomerId { get; set; }
        public  Customer? Customer { get; set; }
        public int? ServiceeId { get; set; }
        public  Servicee? Servicee { get; set; }
        public List<ProjectService> ProjectServices { get; set; } = new List<ProjectService>();


        
    }
}