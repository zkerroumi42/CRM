using System.Collections.Generic;
using System.Threading.Tasks;
using api.Dtos.Opportunity;
using api.Helpers;
using api.interfaces;
using api.models;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OpportunitiesController : ControllerBase
    {
        private readonly IOpportunityRepository _opportunityRepository;

        public OpportunitiesController(IOpportunityRepository opportunityRepository)
        {
            _opportunityRepository = opportunityRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<Opportunity>>> GetAll([FromQuery] QueryObject query)
        {
            var opportunities = await _opportunityRepository.GetAllAsync(query);
            return Ok(opportunities);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Opportunity>> GetById(int id)
        {
            var opportunity = await _opportunityRepository.GetByIdAsync(id);

            if (opportunity == null)
            {
                return NotFound($"Opportunity with ID {id} not found.");
            }

            return Ok(opportunity);
        }

        [HttpPost]
        public async Task<ActionResult<Opportunity>> Create([FromBody] Opportunity opportunityModel)
        {
            var createdOpportunity = await _opportunityRepository.CreateAsync(opportunityModel);
            return CreatedAtAction(nameof(GetById), new { id = createdOpportunity.OpportunityId }, createdOpportunity);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<Opportunity>> Update(int id, [FromBody] UpdateOpportunityRequestDto opportunityDto)
        {
            var updatedOpportunity = await _opportunityRepository.UpdateAsync(id, opportunityDto);

            if (updatedOpportunity == null)
            {
                return NotFound($"Opportunity with ID {id} not found.");
            }

            return Ok(updatedOpportunity);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Opportunity>> Delete(int id)
        {
            var deletedOpportunity = await _opportunityRepository.DeleteAsync(id);

            if (deletedOpportunity == null)
            {
                return NotFound($"Opportunity with ID {id} not found.");
            }

            return Ok(deletedOpportunity);
        }

        [HttpGet("salesperson/{appUserId}")]
        public async Task<ActionResult<List<Opportunity>>> GetBySalesperson(int appUserId)
        {
            var opportunities = await _opportunityRepository.GetBySalesperson(appUserId);
            return Ok(opportunities);
        }

        [HttpGet("client/{clientId}")]
        public async Task<ActionResult<List<Opportunity>>> GetByClient(int clientId)
        {
            var opportunities = await _opportunityRepository.GetByClient(clientId);
            return Ok(opportunities);
        }
        [HttpGet("lead/{leadId}")]
        public async Task<ActionResult<List<Opportunity>>> GetByLead(int leadId)
        {
            var opportunities = await _opportunityRepository.GetByLead(leadId);
            return Ok(opportunities);
        }

        [HttpPut("status/{id}")]
        public async Task<ActionResult<Opportunity>> UpdateStatus(int id, [FromBody] string status)
        {
            var updatedOpportunity = await _opportunityRepository.UpdateStatus(id, status);

            if (updatedOpportunity == null)
            {
                return NotFound($"Opportunity with ID {id} not found.");
            }

            return Ok(updatedOpportunity);
        }
    }
}
