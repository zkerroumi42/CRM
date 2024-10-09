using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.interfaces;
using api.models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class ContactRepository:IContactRepository
    {
        private readonly ApplicationDBContext _context;
        public ContactRepository(ApplicationDBContext context)
        {
            _context=context;
            
        }

        public async Task<Contact> CreateAsync(Contact ContactModel)
        {
            await _context.Contacts.AddAsync(ContactModel);
            await _context.SaveChangesAsync();
            return ContactModel;
        }

        public async Task<Contact?> DeleteAsync(int id)
        {
            var contactModel=await _context.Contacts.FirstOrDefaultAsync(x=>x.ContactId==id);

            if (contactModel==null)
            {
                return null;
            }
            _context.Contacts.Remove(contactModel);
            await _context.SaveChangesAsync();
            return contactModel;
        }

        public async Task<List<Contact>> GetAllAsync()
        {
           return await _context.Contacts.ToListAsync();
        }

        public async Task<Contact?> GetByIdAsync(int id)
        {
             return await _context.Contacts.FindAsync(id);
        }

        public async Task<Contact?> UpdateAsync(int id, Contact contactModel)
        {
            var existingContact=await _context.Contacts.FindAsync(id);
            if (existingContact==null)
            {
                return null;
            }
            existingContact.Name=contactModel.Name;
            existingContact.Email=contactModel.Email;
            existingContact.PhoneNumber=contactModel.PhoneNumber;
            await _context.SaveChangesAsync();
            return existingContact;

        }
    }
}