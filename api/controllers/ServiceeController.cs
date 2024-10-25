using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Servicee;
using api.Helpers;
using api.interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.controllers
{
    [ApiController]
    [Route("api/Servicee")]
    public class ServiceeController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        private readonly IServiceeRepository _ServiceeRepo;
        public ServiceeController(ApplicationDBContext context,IServiceeRepository ServiceeRepo)
        {
            _ServiceeRepo=ServiceeRepo;
            _context=context;
            
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] QueryObject query){
            var Servicee=await _ServiceeRepo.GetAllAsync(query);
            var ServiceeDto=Servicee.Select(s=>s.ToServiceeDto());
            return Ok(ServiceeDto);

        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id){
                        if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var Servicee=await _ServiceeRepo.GetByIdAsync(id);
            if (Servicee==null)
            {
                return NotFound();
                
            }
            return Ok(Servicee);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateServiceeRequestDto ServiceeDto){
                        if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var ServiceeModel=ServiceeDto.ToServiceeFromCreate();
            _ = await _ServiceeRepo.CreateAsync(ServiceeModel);
            return CreatedAtAction(nameof(GetById),new {id=ServiceeModel.ServiceeId},ServiceeModel.ToServiceeDto());

        }
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id,[FromBody] UpdateServiceeRequestDto updateDto){
                        if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var ServiceeModel=await _ServiceeRepo.UpdateAsync(id,updateDto);
            if (ServiceeModel==null)
            {
                return NotFound();
                
            }
            return Ok(ServiceeModel.ToServiceeDto());    



        }
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id){
                        if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var ServiceeModel=await _ServiceeRepo.DeleteAsync(id);

            if (ServiceeModel==null)
            {
                return NotFound();
                
            }
            return NoContent(); 
        }
        
    }
}