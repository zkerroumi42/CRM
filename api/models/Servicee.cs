using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api.models
{
        [Table("Servicees")]

    public class Servicee
    {
        public int ServiceeId { get; set; }
        public string Name { get; set; }=string.Empty;
        [Column(TypeName = "decimal(18,2)")]
        public string Description { get; set; }=string.Empty;
        public int Price { get; set; }
        public List<Review> Reviews { get; set; } = new List<Review>();
        public List<SalaryService> SalaryServices { get; set; } = new List<SalaryService>();
        public List<ProjectService> ProjectServices { get; set; } = new List<ProjectService>();


        
    }
}