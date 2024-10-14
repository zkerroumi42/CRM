using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Sale
{
    public class UpdateSaleRequestDto
    {

        public int Amount { get; set; }
        public DateTime Date { get; set; }
        
    }
}