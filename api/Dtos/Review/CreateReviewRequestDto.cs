using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Review
{
    public class CreateReviewRequestDto
    {
        public int Rating { get; set; }
        public string Comment { get; set; }=string.Empty;
        public DateTime CreateAt { get; set; }
        
    }
}