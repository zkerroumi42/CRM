using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.ProjectService
{
    public class CreateProjectServicenRequestDto
    {
        public int? ProjectId { get; set; }
        public int? ServiceeId { get; set; }
        
    }
}