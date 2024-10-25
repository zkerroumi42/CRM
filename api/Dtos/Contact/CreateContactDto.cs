using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Contact
{
    public class CreateContactDto
    {
        [Required]
        [MinLength(5,ErrorMessage ="this name must be 5 caracters")]
        [MaxLength(20,ErrorMessage ="this name must be 20 caracters")]
        public string Name { get; set; }=string.Empty;
        public string Email { get; set; }=string.Empty;
        public string PhoneNumber { get; set; }=string.Empty;
    }
}