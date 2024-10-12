using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.models
{
    public class Sale
    {
        public int SaleId { get; set; }
        public int Amount { get; set; }
        public DateTime Date { get; set; }
        public int? AppUserId { get; set; }
        public  AppUser? AppUser { get; set; }
        
    }
}