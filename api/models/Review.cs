using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api.models
{
    [Table("Reviews")]

    public class Review
    {
        public int ReviewId { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }=string.Empty;
        public DateTime CreateAt { get; set; }
        public int? CustomerId { get; set; }
        public  Customer? Customer { get; set; }
        public int? ServiceeId { get; set; }
        public  Servicee? Servicee { get; set; }
        
    }
}