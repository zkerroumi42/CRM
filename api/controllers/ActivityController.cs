using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Activity;
using api.Helpers;
using api.interfaces;
using api.Mappers;
using api.models;
using api.Repository;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ActivityController : ControllerBase
    {
        private readonly IActivityRepository _ActivityRepo;
        private readonly ICustomerRepository _CustomerRepo;
        private readonly ILeadRepository _LeadRepo;

        public ActivityController(IActivityRepository ActivityRepo, ICustomerRepository CustomerRepo,ILeadRepository LeadRepo)
        {
            _ActivityRepo = ActivityRepo;
            _CustomerRepo = CustomerRepo;
            _LeadRepo=LeadRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] QueryObject query)
        {
            var Activities = await _ActivityRepo.GetAllAsync(query);
            var ActivityDtos = Activities.Select(s => s.ToActivityDto());
            return Ok(ActivityDtos);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var Activity = await _ActivityRepo.GetByIdAsync(id);
            if (Activity == null)
            {
                return NotFound("Activity not found");
            }

            return Ok(Activity.ToActivityDto());
        }

        [HttpPost("{CustomerId:int}")]
        public async Task<IActionResult> Create([FromRoute] int CustomerId, [FromBody] CreateActivityDto ActivityDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!await _CustomerRepo.CustomerExists(CustomerId))
            {
                return BadRequest("Customer does not exist");
            }

            var ActivityModel = ActivityDto.ToActivityFromCreate(CustomerId);
            _ = await _ActivityRepo.CreateAsync(ActivityModel);
            return CreatedAtAction(nameof(GetById), new { id = ActivityModel.ActivityId }, ActivityModel.ToActivityDto());
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateActivityRequestDto updateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updatedActivity = await _ActivityRepo.UpdateAsync(id, updateDto.ToActivityFromUpdate());

            if (updatedActivity == null)
            {
                return NotFound("Activity not found");
            }

            return Ok(updatedActivity.ToActivityDto());
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var Activity = await _ActivityRepo.DeleteAsync(id);

            if (Activity == null)
            {
                return NotFound("Activity does not exist");
            }

            return Ok(Activity);
        }
        //
        [HttpGet("{customerId}")]
        public async Task<ActionResult<List<Activity>>> GetByCustomerId(int customerId)
        {
            var Activities = await _ActivityRepo.GetByCustomerId(customerId);

            if (Activities == null || Activities.Count == 0)
            {
                return NotFound($"No Activities found for customer with ID {customerId}.");
            }

            return Ok(Activities);
        }
        [HttpGet("{LeadId}")]
        public async Task<ActionResult<List<Activity>>> GetByLeadId(int LeadId)
        {
            var Activities = await _ActivityRepo.GetByLeadId(LeadId);

            if (Activities == null || Activities.Count == 0)
            {
                return NotFound($"No Activities found for customer with ID {LeadId}.");
            }

            return Ok(Activities);
        }
        [HttpGet("{AppUserId}")]
        public async Task<ActionResult<List<Activity>>> GetBySalespersonId(int AppUserId)
        {
            var Activities = await _ActivityRepo.GetBySalespersonId(AppUserId);

            if (Activities == null || Activities.Count == 0)
            {
                return NotFound($"No Activities found for SalesPerson with ID {AppUserId}.");
            }

            return Ok(Activities);
        }
        [HttpGet("day/{date}")]
        public async Task<ActionResult<List<Activity>>> GetByDay(DateTime date){
            var activities = await _ActivityRepo.GetByDay(date);
            return Ok(activities);

        }
        [HttpPatch("{id}/status")]
        public async Task<ActionResult<Activity>> UpdateStatus(int id, [FromBody] UpdateActivityRequestDto statusDto){
            var updatedActivity = await _ActivityRepo.UpdateStatus(id, statusDto.Status);
            if (updatedActivity == null) return NotFound();
            return Ok(updatedActivity);

        }

    }
}
