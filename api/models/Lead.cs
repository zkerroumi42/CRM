using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.models
{
    public class Lead
    {
        public int LeadId { get; set; }
        public string Name { get; set; }=string.Empty;
        public string Status { get; set; }=string.Empty;
        public string LeadSource { get; set; }=string.Empty;
        // public string AssignedTo { get; set; }=string.Empty;
        public string? AppUserId { get; set; }
        public  AppUser? AppUser { get; set; } 
        public int? CampaignId { get; set; }
        public  Campaign? Campaign { get; set; }
        
    }
}