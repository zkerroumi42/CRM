using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.models
{
    public class Campaign
    {
        public int CampaignId { get; set; }
        public string Name { get; set; }=string.Empty;
        public int Budget { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<Lead> Leads { get; set; } = new List<Lead>();
    }
}