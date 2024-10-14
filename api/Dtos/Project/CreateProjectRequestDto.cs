using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Project
{
    public class CreateProjectRequestDto
    {
        public string ProjectName { get; set; }=string.Empty;
        public string Status { get; set; }=string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime CreateAt { get; set; }
        
    }
}