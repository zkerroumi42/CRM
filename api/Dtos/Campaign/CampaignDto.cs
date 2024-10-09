using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Lead;

namespace api.Dtos.Campaign
{
    public class CampaignDto
    {
        public int CampaignId { get; set; }
        public string Name { get; set; }=string.Empty;
        public int Budget { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Description { get; set; }=string.Empty;
        public List<LeadDto> Leads { get; set; }
        
    }
}