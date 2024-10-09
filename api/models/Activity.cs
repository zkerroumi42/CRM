using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.models
{
    public class Activity
    {
        public int ActivityId { get; set; }
        public string Type { get; set; }=string.Empty;
        public DateTime Date { get; set; }
        public string Description { get; set; }=string.Empty;
        public int? OpportunityId { get; set; }
        public  Opportunity? Opportunity { get; set; } 
        public int? LeadId { get; set; }
        public  Lead? Lead { get; set; } 

        
    }
}