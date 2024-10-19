using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Activity;
using api.Helpers;
using api.interfaces;
using api.models;
using Microsoft.AspNetCore.Mvc;

namespace api.controllers
{
    [ApiController]
    [Route("api/activities")]
    public class ActivitiesController : ControllerBase
    {
        private readonly IActivityRepository _activityRepository;

        public ActivitiesController(IActivityRepository activityRepository)
        {
            _activityRepository = activityRepository;
        }


        [HttpGet]
        public async Task<ActionResult<List<Activity>>> GetAll([FromQuery] QueryObject query)
        {
            var activities = await _activityRepository.GetAllAsync(query);
            return Ok(activities);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Activity>> GetById(int id)
        {
            var activity = await _activityRepository.GetByIdAsync(id);
            if (activity == null) return NotFound();
            return Ok(activity);
        }
        [HttpPost]
        public async Task<ActionResult<Activity>> Create([FromBody] CreateActivityDto activityDto)
        {
            var activity = new Activity
            {
                Type = activityDto.Type,
                Description = activityDto.Description,
                DueDate = activityDto.Date,
                Status = activityDto.Status,
                CustomerId = activityDto.CustomerId,
                LeadId = activityDto.LeadId,
                AppUserId = activityDto.AppUserId
            };

            var createdActivity = await _activityRepository.CreateAsync(activity);
            return CreatedAtAction(nameof(GetById), new { id = createdActivity.ActivityId }, createdActivity);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<Activity>> Update(int id, [FromBody] UpdateActivityRequestDto activityDto)
        {
            var updatedActivity = await _activityRepository.UpdateAsync(id, activityDto);
            if (updatedActivity == null) return NotFound();
            return Ok(updatedActivity);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Activity>> Delete(int id)
        {
            var deletedActivity = await _activityRepository.DeleteAsync(id);
            if (deletedActivity == null) return NotFound();
            return Ok(deletedActivity);
        }

        [HttpGet("client/{customerId}")]
        public async Task<ActionResult<List<Activity>>> GetByClient(int customerId)
        {
            var activities = await _activityRepository.GetByClient(customerId);
            return Ok(activities);
        }
        [HttpGet("lead/{leadId}")]
        public async Task<ActionResult<List<Activity>>> GetByLead(int leadId)
        {
            var activities = await _activityRepository.GetByLead(leadId);
            return Ok(activities);
        }

        [HttpGet("salesperson/{salespersonId}")]
        public async Task<ActionResult<List<Activity>>> GetBySalesperson(int salespersonId)
        {
            var activities = await _activityRepository.GetBySalesperson(salespersonId);
            return Ok(activities);
        }

        [HttpGet("day/{date}")]
        public async Task<ActionResult<List<Activity>>> GetByDay(DateTime date)
        {
            var activities = await _activityRepository.GetByDay(date);
            return Ok(activities);
        }

        [HttpPatch("{id}/status")]
        public async Task<ActionResult<Activity>> UpdateStatus(int id, [FromBody] UpdateActivityRequestDto statusDto)
        {
            var updatedActivity = await _activityRepository.UpdateStatus(id, statusDto.Status);
            if (updatedActivity == null) return NotFound();
            return Ok(updatedActivity);
        }
    }
}