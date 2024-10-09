using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Campaign;
using api.Helpers;
using api.interfaces;
using api.mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.controllers
{
    [ApiController]
    [Route("api/campaign")]
    public class CampaignController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        private readonly ICampaignRepository _CampaignRepo;
        public CampaignController(ApplicationDBContext context,ICampaignRepository CampaignRepo)
        {
            _CampaignRepo=CampaignRepo;
            _context=context;
            
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll([FromQuery] QueryObject query){
            var Campaign=await _CampaignRepo.GetAllAsync(query);
            var CampaignDto=Campaign.Select(s=>s.ToCampaignDto());
            return Ok(CampaignDto);

        }
        [HttpGet("{id:int}")]
        [Authorize]
        public async Task<IActionResult> GetById([FromRoute] int id){
                        if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var Campaign=await _CampaignRepo.GetByIdAsync(id);
            if (Campaign==null)
            {
                return NotFound();
                
            }
            return Ok(Campaign);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCampaignRequestDto campaignDto){
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var CampaignModel=campaignDto.ToCampaignFromCreateDto();
            await _CampaignRepo.CreateAsync(CampaignModel);
            return CreatedAtAction(nameof(GetById),new {id=CampaignModel.CampaignId},CampaignModel.ToCampaignDto());

        }
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id,[FromBody] UpdateCampaignRequestDto updateDto){
                        if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var CampaignModel=await _CampaignRepo.UpdateAsync(id,updateDto);
            if (CampaignModel==null)
            {
                return NotFound();
                
            }
            return Ok(CampaignModel.ToCampaignDto());    



        }
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id){
                        if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var CampaignModel=await _CampaignRepo.DeleteAsync(id);

            if (CampaignModel==null)
            {
                return NotFound();
                
            }
            return NoContent(); 
        }
        
    }
}