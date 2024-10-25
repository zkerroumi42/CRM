using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Review;
using api.Helpers;
using api.models;

namespace api.interfaces
{
    public interface IReviewRepository
    {
        Task<List<Review>>GetAllAsync(QueryObject query);
        Task<Review>GetByIdAsync(int id);
        Task<Review>CreateAsync(Review ReviewModel);
        Task<Review>UpdateAsync(int id,Review ReviewDto);
        Task<Review>DeleteAsync(int id);
        Task<List<Review>> GetByServiceId(int serviceId);
        
    }
}