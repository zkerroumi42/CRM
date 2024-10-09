using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Customer;
using api.Helpers;
using api.interfaces;
using api.mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.controllers
{
    [ApiController]
    [Route("api/customer")]
    public class CustomerController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        private readonly ICustomerRepository _customerRepo;
        public CustomerController(ApplicationDBContext context,ICustomerRepository customerRepo)
        {
            _customerRepo=customerRepo;
            _context=context;
            
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll([FromQuery] QueryObject query){
            var Customer=await _customerRepo.GetAllAsync(query);
            var customerDto=Customer.Select(s=>s.ToCustomerDto());
            return Ok(customerDto);

        }
        [HttpGet("{id:int}")]
        [Authorize]
        public async Task<IActionResult> GetById([FromRoute] int id){
                        if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var customer=await _customerRepo.GetByIdAsync(id);
            if (customer==null)
            {
                return NotFound();
                
            }
            return Ok(customer);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCustomerRequestDto customerDto){
                        if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var customerModel=customerDto.ToCustomerFromCreateDto();
            await _customerRepo.CreateAsync(customerModel);
            return CreatedAtAction(nameof(GetById),new {id=customerModel.CustomerId},customerModel.ToCustomerDto());

        }
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id,[FromBody] UpdateCustomerRequestDto updateDto){
                        if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var customerModel=await _customerRepo.UpdateAsync(id,updateDto);
            if (customerModel==null)
            {
                return NotFound();
                
            }
            return Ok(customerModel.ToCustomerDto());    



        }
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id){
                        if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var customerModel=await _customerRepo.DeleteAsync(id);

            if (customerModel==null)
            {
                return NotFound();
                
            }
            return NoContent(); 
        }
        
    }
}