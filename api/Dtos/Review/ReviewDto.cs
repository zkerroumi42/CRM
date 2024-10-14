using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Lead;

namespace api.Dtos.Review
{
    public class ReviewDto
    {
        public int ReviewId { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }=string.Empty;
        public DateTime CreateAt { get; set; }
        
    }
}