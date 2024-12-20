using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api.models
{
    [Table("Customers")]
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }
        public string CompanyName { get; set; }=string.Empty;
         public string ContactPerson { get; set; }=string.Empty;
        public string Industry { get; set; }=string.Empty;
        public string Email { get; set; }=string.Empty;
        public string PhoneNumber { get; set; }=string.Empty;
        public string Address { get; set; }=string.Empty;
        public string Website { get; set; }=string.Empty;
        public DateTime CreateAt { get; set; }
        public List<Contact> Contacts { get; set; } = new List<Contact>();
        public List<Opportunity> Opportunities { get; set; } = new List<Opportunity>();     
    }
}