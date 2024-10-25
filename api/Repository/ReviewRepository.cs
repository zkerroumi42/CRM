using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Helpers;
using api.interfaces;
using api.models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class ReviewRepository:IReviewRepository
    {
        private readonly ApplicationDBContext _context;
        public ReviewRepository(ApplicationDBContext context)
        {
            _context=context;
            
        }

        public async Task<Review> CreateAsync(Review ReviewModel)
        {
            _ = await _context.Reviews.AddAsync(ReviewModel);
            _ = await _context.SaveChangesAsync();
            return ReviewModel;
        }

        public async Task<Review?> DeleteAsync(int id)
        {
            var ReviewModel=await _context.Reviews.FirstOrDefaultAsync(x=>x.ReviewId==id);

            if (ReviewModel==null)
            {
                return null;
            }
            _ = _context.Reviews.Remove(ReviewModel);
            _ = await _context.SaveChangesAsync();
            return ReviewModel;
        }

        public async Task<List<Review>> GetAllAsync(QueryObject query)
        {
            var Reviews = _context.Reviews.Include(a => a.Customer).AsQueryable();
            if (!string.IsNullOrWhiteSpace(query.SortBy))
            {
                Reviews = query.IsDecending
                    ? Reviews.OrderByDescending(c => EF.Property<object>(c, query.SortBy))
                    : Reviews.OrderBy(c => EF.Property<object>(c, query.SortBy));
            }

            var skipNumber = (query.PageNumber - 1) * query.PageSize;
            return await Reviews.Skip(skipNumber).Take(query.PageSize).ToListAsync();
                
                }

        public async Task<List<Review>> GetByServiceId(int serviceId)
        {
                return await _context.Reviews
                         .Where(c => c.ServiceeId == serviceId) 
                         .ToListAsync();
        }

        public async Task<Review?> GetByIdAsync(int id)
        {
             return await _context.Reviews.Include(a => a.CustomerId).FirstOrDefaultAsync(c => c.ReviewId == id);
        }

        public async Task<Review?> UpdateAsync(int id, Review ReviewModel)
        {
            var existingReview=await _context.Reviews.FindAsync(id);
            if (existingReview==null)
            {
                return null;
            }
            existingReview.Comment=ReviewModel.Comment;
            existingReview.Rating=ReviewModel.Rating;
            existingReview.CreateAt=ReviewModel.CreateAt;
            existingReview.CustomerId=ReviewModel.CustomerId;
            _ = await _context.SaveChangesAsync();
            return existingReview;

        }
    }
}