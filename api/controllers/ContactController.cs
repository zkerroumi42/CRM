using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Contact;
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
    public class ContactController : ControllerBase
    {
        private readonly IContactRepository _contactRepo;
        private readonly ICustomerRepository _CustomerRepo;

        public ContactController(IContactRepository contactRepo, ICustomerRepository CustomerRepo)
        {
            _contactRepo = contactRepo;
            _CustomerRepo = CustomerRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] QueryObject query)
        {
            var contacts = await _contactRepo.GetAllAsync(query);
            var contactDtos = contacts.Select(s => s.ToContactDto());
            return Ok(contactDtos);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var contact = await _contactRepo.GetByIdAsync(id);
            if (contact == null)
            {
                return NotFound("Contact not found");
            }

            return Ok(contact.ToContactDto());
        }

        [HttpPost("{CustomerId:int}")]
        public async Task<IActionResult> Create([FromRoute] int CustomerId, [FromBody] CreateContactDto contactDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!await _CustomerRepo.CustomerExists(CustomerId))
            {
                return BadRequest("Customer does not exist");
            }

            var contactModel = contactDto.ToContactFromCreate(CustomerId);
            _ = await _contactRepo.CreateAsync(contactModel);
            return CreatedAtAction(nameof(GetById), new { id = contactModel.ContactId }, contactModel.ToContactDto());
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateContactRequestDto updateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updatedContact = await _contactRepo.UpdateAsync(id, updateDto.ToContactFromUpdate());

            if (updatedContact == null)
            {
                return NotFound("Contact not found");
            }

            return Ok(updatedContact.ToContactDto());
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var contact = await _contactRepo.DeleteAsync(id);

            if (contact == null)
            {
                return NotFound("Contact does not exist");
            }

            return Ok(contact);
        }
        //
        [HttpGet("{customerId}")]
        public async Task<ActionResult<List<Contact>>> GetByCustomerId(int customerId)
        {
            var contacts = await _contactRepo.GetByCustomerId(customerId);

            if (contacts == null || contacts.Count == 0)
            {
                return NotFound($"No contacts found for customer with ID {customerId}.");
            }

            return Ok(contacts);
        }
    }
}
