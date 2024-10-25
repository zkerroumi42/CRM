using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Customer
{
    public class UpdateCustomerRequestDto
    {
        public string CompanyName { get; set; }=string.Empty;
        public string Industry { get; set; }=string.Empty;
        public string Email { get; set; }=string.Empty;
        public string PhoneNumber { get; set; }=string.Empty;
        public string Address { get; set; }=string.Empty;
        public string Website { get; set; }=string.Empty;
    }
}