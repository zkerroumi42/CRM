using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.models
{
    public class Target
    {
    public int TargetId { get; set; }  
    public decimal Goal { get; set; }  
    public string Type { get; set; }=string.Empty;  
    public DateTime DueDate { get; set; }
        
        
    }
}