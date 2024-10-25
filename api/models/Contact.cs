using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api.models
{
    [Table("Contacts")]
    public class Contact
    {
        public int ContactId { get; set; }
        public string Name { get; set; }=string.Empty;
        public string Email { get; set; }=string.Empty;
        public string PhoneNumber { get; set; }=string.Empty;
        public string RoleAtCompany { get; set; }=string.Empty;
        public int? CustomerId { get; set; }
        public  Customer? Customer { get; set; }
        
    }
}