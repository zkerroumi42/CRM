using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Lead;

namespace api.Dtos.SalaryService
{
    public class SalaryServiceDto
    {
        public int SalaryServiceId { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public int? AppUserId { get; set; }
        public int? ServiceeId { get; set; }
        
    }
}