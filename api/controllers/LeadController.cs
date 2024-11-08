using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Lead;
using api.Helpers;
using api.interfaces;
using api.Mappers;
using api.models;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("api/lead")]
    public class LeadController : ControllerBase
    {
        private readonly ILeadRepository _LeadRepo;
        private readonly ICampaignRepository _CampaignRepo;

        public LeadController(ILeadRepository LeadRepo, ICampaignRepository CampaignRepo)
        {
            _LeadRepo = LeadRepo;
            _CampaignRepo = CampaignRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] QO1 query)
        {
            var Leads = await _LeadRepo.GetAllAsync(query);
            var LeadDtos = Leads.Select(s => s.ToLeadDto());
            return Ok(LeadDtos);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var Lead = await _LeadRepo.GetByIdAsync(id);
            if (Lead == null)
            {
                return NotFound("Lead not found");
            }

            return Ok(Lead.ToLeadDto());
        }

        [HttpPost("{CampaignId:int}")]
        public async Task<IActionResult> Create([FromRoute] int CampaignId, [FromBody] CreateLeadDto LeadDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!await _CampaignRepo.CampaignExists(CampaignId))
            {
                return BadRequest("Campaign does not exist");
            }

            var LeadModel = LeadDto.ToLeadFromCreate(CampaignId);
            _ = await _LeadRepo.CreateAsync(LeadModel);
            return CreatedAtAction(nameof(GetById), new { id = LeadModel.LeadId }, LeadModel.ToLeadDto());
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateLeadRequestDto updateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updatedLead = await _LeadRepo.UpdateAsync(id, updateDto.ToLeadFromUpdate());

            if (updatedLead == null)
            {
                return NotFound("Lead not found");
            }

            return Ok(updatedLead.ToLeadDto());
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var Lead = await _LeadRepo.DeleteAsync(id);

            if (Lead == null)
            {
                return NotFound("Lead does not exist");
            }

            return Ok(Lead);
        }

        //
        [HttpGet("{status}")]
        public async Task<ActionResult<Lead>> GetByStatus(string status)
        {
            var lead = await _LeadRepo.GetByStatus(status);

            if (lead == null)
            {
                return NotFound($"No lead found with status '{status}'.");
            }

            return Ok(lead);
        }

    }
}
