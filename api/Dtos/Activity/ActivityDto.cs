using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Activity
{
    public class ActivityDto
    {
        public int ActivityId { get; set; }
        public string Type { get; set; }=string.Empty;
        public DateTime Date { get; set; }
        public string Description { get; set; }=string.Empty;
        public string Status { get; set; }=string.Empty;
        public int? LeadId { get; set; }
        public int? AppUserId { get; set; }
        public int? CustomerId { get; set; }
        
    }
}