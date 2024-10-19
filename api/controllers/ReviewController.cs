using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Review;
using api.Helpers;
using api.interfaces;
using api.Mappers;
using api.models;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewRepository _reviewRepo;
        private readonly IServiceeRepository _serviceRepo;

        public ReviewController(IReviewRepository reviewRepo, IServiceeRepository serviceRepo)
        {
            _reviewRepo = reviewRepo;
            _serviceRepo = serviceRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] QueryObject query)
        {
            var reviews = await _reviewRepo.GetAllAsync(query);
            var reviewDtos = reviews.Select(r => r.ToReviewDto());
            return Ok(reviewDtos);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var review = await _reviewRepo.GetByIdAsync(id);
            if (review == null)
            {
                return NotFound("Review not found");
            }

            return Ok(review.ToReviewDto());
        }

        [HttpPost("{serviceId:int}")]
        public async Task<IActionResult> Create([FromRoute] int serviceId, [FromBody] CreateReviewRequestDto reviewDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!await _serviceRepo.ServiceeExists(serviceId))
            {
                return BadRequest("Service does not exist");
            }

            var reviewModel = reviewDto.ToReviewFromCreate(serviceId);
            _ = await _reviewRepo.CreateAsync(reviewModel);
            return CreatedAtAction(nameof(GetById), new { id = reviewModel.ReviewId }, reviewModel.ToReviewDto());
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateReviewRequestDto updateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updatedReview = await _reviewRepo.UpdateAsync(id, updateDto.ToReviewFromUpdate());

            if (updatedReview == null)
            {
                return NotFound("Review not found");
            }

            return Ok(updatedReview.ToReviewDto());
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var review = await _reviewRepo.DeleteAsync(id);

            if (review == null)
            {
                return NotFound("Review does not exist");
            }

            return Ok(review);
        }

        [HttpGet("service/{serviceId:int}")]
        public async Task<IActionResult> GetByService(int serviceId)
        {
            var reviews = await _reviewRepo.GetByServiceId(serviceId);

            if (reviews == null || reviews.Count == 0)
            {
                return NotFound($"No reviews found for service with ID {serviceId}.");
            }

            return Ok(reviews.Select(r => r.ToReviewDto()));
        }
    }
}
