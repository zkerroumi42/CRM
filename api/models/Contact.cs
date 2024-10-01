using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.models
{
    public class Contact
    {
        public int ContactId { get; set; }
        public string Name { get; set; }=string.Empty;
        public string Email { get; set; }=string.Empty;
        public string PhoneNumber { get; set; }=string.Empty;
        public int? AccountId { get; set; }
        public  Account? Account { get; set; }
        
    }
}