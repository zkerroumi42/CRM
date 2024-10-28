using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api.models
{
        [Table("Sales")]

    public class Sale
    {
        [Key]
        public int SaleId { get; set; }
        public int Amount { get; set; }
        public DateTime Date { get; set; }
        public int? AppUserId { get; set; }
        public  AppUser? AppUser { get; set; }
        public int? ProjectId { get; set; }
        public  Project? Project { get; set; }
        
    }
}