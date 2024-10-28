using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api.models
{
        [Table("SalaryServices")]

    public class SalaryService
    {
        [Key]
        public int SalaryServiceId { get; set; }
        public DateTime DateStart { get; set; }=DateTime.Now;
        public DateTime DateEnd { get; set; }
        public string? AppUserId { get; set; }
        public  AppUser? AppUser { get; set; }
        public int? ServiceeId { get; set; }
        public  Servicee? Servicee { get; set; }
        
    }
}