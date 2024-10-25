using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Helpers;
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
            _ = await _context.Contacts.AddAsync(ContactModel);
            _ = await _context.SaveChangesAsync();
            return ContactModel;
        }

        public async Task<Contact?> DeleteAsync(int id)
        {
            var contactModel=await _context.Contacts.FirstOrDefaultAsync(x=>x.ContactId==id);

            if (contactModel==null)
            {
                return null;
            }
            _ = _context.Contacts.Remove(contactModel);
            _ = await _context.SaveChangesAsync();
            return contactModel;
        }

        public async Task<List<Contact>> GetAllAsync(QueryObject query)
        {
            var contacts = _context.Contacts.AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.CompanyName))
            {
                contacts = contacts.Where(c => c.Name.Contains(query.CompanyName));
            }
            if (!string.IsNullOrWhiteSpace(query.SortBy))
            {
                contacts = query.IsDecending
                    ? contacts.OrderByDescending(c => EF.Property<object>(c, query.SortBy))
                    : contacts.OrderBy(c => EF.Property<object>(c, query.SortBy));
            }

            var skipNumber = (query.PageNumber - 1) * query.PageSize;
            return await contacts.Skip(skipNumber).Take(query.PageSize).ToListAsync();
                
                }

        public async Task<List<Contact>> GetByCustomerId(int customerId)
        {
                return await _context.Contacts
                         .Where(c => c.CustomerId == customerId) 
                         .ToListAsync();
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
            _ = await _context.SaveChangesAsync();
            return existingContact;

        }
    }
}