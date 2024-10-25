using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Contact
{
    public class ContactDto
    {
        
        public int ContactId { get; set; }
        public string Name { get; set; }=string.Empty;
        public string Email { get; set; }=string.Empty;
        public string PhoneNumber { get; set; }=string.Empty;
        public int? CustomerId { get; set; }
        
    }
}