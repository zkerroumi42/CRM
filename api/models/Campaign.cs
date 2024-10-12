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
        public string Description { get; set; }=string.Empty;
        public string status { get; set; }=string.Empty;
        public String Goal { get; set; }=string.Empty;
        public DateTime CreateAt { get; set; }

        public List<Lead> Leads { get; set; } = new List<Lead>();
    }
}