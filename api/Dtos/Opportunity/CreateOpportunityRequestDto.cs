using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Opportunity
{
    public class CreateOpportunityRequestDto
    {
        public string Name { get; set; }=string.Empty;  
        public decimal Value { get; set; }  
        public string Probability { get; set; }=string.Empty;    
        public DateTime CreatedAt { get; set; }  
        public DateTime? CloseDate { get; set; }  
        public string Status { get; set; }=string.Empty;
    }
}