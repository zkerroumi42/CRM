using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using api.Helpers;

namespace api.models
{
        [Table("Activities")]

    public class Activity
    {
        [Key]
        public int ActivityId { get; set; }
        public string Type { get; set; }=string.Empty;
        public DateTime DueDate { get; set; }
        public string Description { get; set; }=string.Empty;
        public string Status { get; set; }=ActivityStatus.Pending;
        public int? LeadId { get; set; }
        public  Lead? Lead { get; set; } 
        public int? AppUserId { get; set; }
        public  AppUser? AppUser { get; set; } 
        public int? CustomerId { get; set; }
        public  Customer? Customer { get; set; } 

        
    }
}