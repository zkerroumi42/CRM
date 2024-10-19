using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api.models
{
        [Table("Opportunities")]

    public class Opportunity
    {
    public int OpportunityId { get; set; }  
    public string Name { get; set; }=string.Empty;  
    public decimal Value { get; set; }  
    public string Probability { get; set; }=string.Empty;    
    public DateTime CreatedAt { get; set; }  
    public DateTime? CloseDate { get; set; }  
    public string Status { get; set; }=string.Empty;
    public int? LeadId { get; set; }
    public  Lead? Lead { get; set; }    
    public int? CustomerId { get; set; }
    public  Customer? Customer { get; set; }
    public int? AppUserId { get; set; }
    public  AppUser? AppUser { get; set; }  
          
    }
}