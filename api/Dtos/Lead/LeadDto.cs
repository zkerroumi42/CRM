using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Lead
{
    public class LeadDto
    {
        public int LeadId { get; set; }
        public string Name { get; set; }=string.Empty;
        public string Status { get; set; }=string.Empty;
        public string Source { get; set; }=string.Empty;
        public int? CampaignId { get; set; }
        
    }
}