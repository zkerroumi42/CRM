using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Lead;

namespace api.Dtos.Sale
{
    public class SaleDto
    {
        public int SaleId { get; set; }
        public int Amount { get; set; }
        public DateTime Date { get; set; }
        
    }
}