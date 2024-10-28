using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using api.Helpers;

namespace api.models
{
        [Table("Leads")]

    public class Lead
    {
        [Key]
        public int LeadId { get; set; }
        public string Name { get; set; }=string.Empty;
        public string Status { get; set; }=string.Empty;
        public string LeadSource { get; set; }=LeadSources.Website;
        // public string AssignedTo { get; set; }=string.Empty;
        public string? AppUserId { get; set; }
        public  AppUser? AppUser { get; set; } 
        public int? CampaignId { get; set; }
        public  Campaign? Campaign { get; set; }
        
    }
}