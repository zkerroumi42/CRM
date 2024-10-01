using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.models
{
    public class Account
    {
        public int AccountId { get; set; }
        public string CompanyName { get; set; }=string.Empty;
        public string Industry { get; set; }=string.Empty;
        public string Email { get; set; }=string.Empty;
        public string PhoneNumber { get; set; }=string.Empty;
        public string Address { get; set; }=string.Empty;
        public string Website { get; set; }=string.Empty;
        public List<Contact> Contacts { get; set; } = new List<Contact>();
        public List<Opportunity> Opportunities { get; set; } = new List<Opportunity>();
        
    }
}