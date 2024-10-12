using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.models
{
    public class SalaryService
    {
        public int SalaryServiceId { get; set; }
        public int? AppUserId { get; set; }
        public  AppUser? AppUser { get; set; }
        public int? ServiceeId { get; set; }
        public  Servicee? Servicee { get; set; }
        
    }
}