using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Review;
using api.models;

namespace api.Mappers
{
    public static class ReviewMappers
    {
        public static ReviewDto ToReviewDto(this Review ReviewModel){
            return new ReviewDto{
                ReviewId=ReviewModel.ReviewId,
                Rating=ReviewModel.Rating,
                Comment=ReviewModel.Comment,
                CreateAt=ReviewModel.CreateAt,

            };
        }
        public static Review ToReviewFromCreate(this CreateReviewRequestDto ReviewDto,int ServiceId){
            return new Review
            {
                Rating=ReviewDto.Rating,
                Comment=ReviewDto.Comment,
                CreateAt=ReviewDto.CreateAt,
                ServiceeId=ServiceId
            };
        }

        public static Review ToReviewFromUpdate(this UpdateReviewRequestDto ReviewDto){
            return new Review
            {
                Rating=ReviewDto.Rating,
                Comment=ReviewDto.Comment,
                CreateAt=ReviewDto.CreateAt,
            };
        }
        

        
    }
}