using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Opportunity;
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
    public class OpportunityController : ControllerBase
    {
        private readonly IOpportunityRepository _OpportunityRepo;
        private readonly ICustomerRepository _CustomerRepo;
        private readonly ILeadRepository _LeadRepo;

        public OpportunityController(IOpportunityRepository OpportunityRepo, ICustomerRepository CustomerRepo,ILeadRepository LeadRepo)
        {
            _OpportunityRepo = OpportunityRepo;
            _CustomerRepo = CustomerRepo;
            _LeadRepo=LeadRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] QueryObject query)
        {
            var Activities = await _OpportunityRepo.GetAllAsync(query);
            var OpportunityDtos = Activities.Select(s => s.ToOpportunityDto());
            return Ok(OpportunityDtos);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var Opportunity = await _OpportunityRepo.GetByIdAsync(id);
            if (Opportunity == null)
            {
                return NotFound("Opportunity not found");
            }

            return Ok(Opportunity.ToOpportunityDto());
        }

        [HttpPost("{CustomerId:int}")]
        public async Task<IActionResult> Create([FromRoute] int CustomerId, [FromBody] CreateOpportunityRequestDto OpportunityDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!await _CustomerRepo.CustomerExists(CustomerId))
            {
                return BadRequest("Customer does not exist");
            }

            var OpportunityModel = OpportunityDto.ToOpportunityFromCreate(CustomerId);
            _ = await _OpportunityRepo.CreateAsync(OpportunityModel);
            return CreatedAtAction(nameof(GetById), new { id = OpportunityModel.OpportunityId }, OpportunityModel.ToOpportunityDto());
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateOpportunityRequestDto updateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updatedOpportunity = await _OpportunityRepo.UpdateAsync(id, updateDto.ToOpportunityFromUpdate());

            if (updatedOpportunity == null)
            {
                return NotFound("Opportunity not found");
            }

            return Ok(updatedOpportunity.ToOpportunityDto());
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var Opportunity = await _OpportunityRepo.DeleteAsync(id);

            if (Opportunity == null)
            {
                return NotFound("Opportunity does not exist");
            }

            return Ok(Opportunity);
        }
        //
        [HttpGet("{customerId}")]
        public async Task<ActionResult<List<Opportunity>>> GetByCustomerId(int customerId)
        {
            var Activities = await _OpportunityRepo.GetByCustomerId(customerId);

            if (Activities == null || Activities.Count == 0)
            {
                return NotFound($"No Activities found for customer with ID {customerId}.");
            }

            return Ok(Activities);
        }
        [HttpGet("{LeadId}")]
        public async Task<ActionResult<List<Opportunity>>> GetByLeadId(int LeadId)
        {
            var Activities = await _OpportunityRepo.GetByLeadId(LeadId);

            if (Activities == null || Activities.Count == 0)
            {
                return NotFound($"No Activities found for customer with ID {LeadId}.");
            }

            return Ok(Activities);
        }
        [HttpGet("{AppUserId}")]
        public async Task<ActionResult<List<Opportunity>>> GetBySalespersonId(int AppUserId)
        {
            var Activities = await _OpportunityRepo.GetBySalespersonId(AppUserId);

            if (Activities == null || Activities.Count == 0)
            {
                return NotFound($"No Activities found for SalesPerson with ID {AppUserId}.");
            }

            return Ok(Activities);
        }
        [HttpGet("day/{date}")]
        public async Task<ActionResult<List<Opportunity>>> GetByDay(DateTime date){
            var activities = await _OpportunityRepo.GetByDay(date);
            return Ok(activities);

        }
        [HttpPatch("{id}/status")]
        public async Task<ActionResult<Opportunity>> UpdateStatus(int id, [FromBody] UpdateOpportunityRequestDto statusDto){
            var updatedOpportunity = await _OpportunityRepo.UpdateStatus(id, statusDto.Status);
            if (updatedOpportunity == null) return NotFound();
            return Ok(updatedOpportunity);

        }

    }
}
