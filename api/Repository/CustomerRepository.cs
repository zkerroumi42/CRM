using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Customer;
using api.Helpers;
using api.interfaces;
using api.models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
         private readonly ApplicationDBContext _context;
        public CustomerRepository(ApplicationDBContext context)
        {
            _context=context;
            
        }

        public Task<bool> CustomerExists(int id)
        {
            return _context.Customers.AnyAsync(s=>s.CustomerId==id);
        }

        public async Task<Customer> CreateAsync(Customer CustomerModel)
        {
            await _context.Customers.AddAsync(CustomerModel);
            await _context.SaveChangesAsync();
            return CustomerModel;
        }


        public async Task<Customer> DeleteAsync(int id)
        {
            var CustomerModel=await _context.Customers.FirstOrDefaultAsync(x=>x.CustomerId==id);

            if (CustomerModel==null)
            {
                return null;
                
            }
            _context.Customers.Remove(CustomerModel);
            await _context.SaveChangesAsync();
            return CustomerModel;
        }

        public async Task<List<Customer>> GetAllAsync(QueryObject query)
        {
            var Customers=_context.Customers.Include(c=>c.Contacts).AsQueryable();
            if(!string.IsNullOrWhiteSpace(query.CompanyName))
            {
                Customers=Customers.Where(s=>s.CompanyName.Contains(query.CompanyName));
            }
            if (!string.IsNullOrWhiteSpace(query.SortBy))
            {
                if (query.SortBy.Equals("CompanyName",StringComparison.OrdinalIgnoreCase))
                {
                    Customers=query.IsDecending ? Customers.OrderByDescending(s=>s.CompanyName):Customers.OrderBy(s=>s.CompanyName);
                    
                }

   
            }
            var skipNumber=(query.PageNumber-1)*query.PageSize;

            return await Customers.Skip(skipNumber).Take(query.PageSize).ToListAsync();
            
        }

        public async Task<Customer?> GetByIdAsync(int id)
        {
            return await _context.Customers.Include(c=>c.Contacts).FirstOrDefaultAsync(i=>i.CustomerId==id);
        }

        public async Task<Customer?> UpdateAsync(int id, UpdateCustomerRequestDto updateDto)
        {
            var existingCustomer=await _context.Customers.FirstOrDefaultAsync(x=>x.CustomerId==id);

            if(existingCustomer==null){
                return null;
            }
            existingCustomer.CompanyName=updateDto.CompanyName;
            existingCustomer.Industry=updateDto.Industry;
            existingCustomer.Email=updateDto.Email;
            existingCustomer.PhoneNumber=updateDto.PhoneNumber;
            existingCustomer.Address=updateDto.Address;
            existingCustomer.Website=updateDto.Website;

            await _context.SaveChangesAsync();

            return existingCustomer;
        }
    }
}