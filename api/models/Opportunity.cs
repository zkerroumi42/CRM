using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.models
{
    public class Opportunity
    {
    public int OpportunityId { get; set; }  
    public string Name { get; set; }=string.Empty;  
    public decimal Value { get; set; }  
    public string Stage { get; set; }=string.Empty;    
    public DateTime CreatedDate { get; set; }  
    public DateTime? CloseDate { get; set; }  
    public string Status { get; set; }=string.Empty;    
    public int? CustomerId { get; set; }
    public  Customer? Customer { get; set; }  
          
    }
}