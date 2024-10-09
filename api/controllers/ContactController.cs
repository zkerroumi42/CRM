using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Contact;
using api.interfaces;
using api.Mappers;
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
        public async Task<IActionResult> GetAll()
        {
            var contacts = await _contactRepo.GetAllAsync();
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

            var contactModel = contactDto.ToContacFromCreate(CustomerId);
            await _contactRepo.CreateAsync(contactModel);
            return CreatedAtAction(nameof(GetById), new { id = contactModel.ContactId }, contactModel.ToContactDto());
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateContactRequestDto updateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updatedContact = await _contactRepo.UpdateAsync(id, updateDto.ToContacFromUpdate());

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
    }
}
