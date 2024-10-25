using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.SalaryService
{
    public class CreateSalaryServiceRequestDto
    {
        public DateTime DateStart { get; set; }=DateTime.Now;
        public DateTime DateEnd { get; set; }
        public string? AppUserId { get; set; }
        public int? ServiceeId { get; set; }
        
    }
}