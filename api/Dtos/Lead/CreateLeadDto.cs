using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Lead
{
    public class CreateLeadDto
    {
        [Required]
        [MinLength(5,ErrorMessage ="this name must be 5 caracters")]
        [MaxLength(20,ErrorMessage ="this name must be 20 caracters")]
        public string Name { get; set; }=string.Empty;
        public string Status { get; set; }=string.Empty;
        public string Source { get; set; }=string.Empty;
        
    }
}