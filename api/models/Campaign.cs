using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using api.Helpers;

namespace api.models
{
        [Table("Campaigns")]

    public class Campaign
    {
        [Key]
        public int CampaignId { get; set; }
        public string Name { get; set; }=string.Empty;
        public int Budget { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Description { get; set; }=string.Empty;
        public string status { get; set; }=CampaignStatus.Planned;
        public string Goal { get; set; }=string.Empty;
        public DateTime CreateAt { get; set; }

        public List<Lead> Leads { get; set; } = new List<Lead>();
    }
}