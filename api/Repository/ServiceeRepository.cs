using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Servicee;
using api.Helpers;
using api.interfaces;
using api.models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class ServiceeRepository : IServiceeRepository
    {
         private readonly ApplicationDBContext _context;
        public ServiceeRepository(ApplicationDBContext context)
        {
            _context=context;
            
        }

        public Task<bool> ServiceeExists(int id)
        {
            return _context.Servicees.AnyAsync(s=>s.ServiceeId==id);
        }

        public async Task<Servicee> CreateAsync(Servicee ServiceeModel)
        {
            _ = await _context.Servicees.AddAsync(ServiceeModel);
            _ = await _context.SaveChangesAsync();
            return ServiceeModel;
        }


        public async Task<Servicee> DeleteAsync(int id)
        {
            var ServiceeModel=await _context.Servicees.FirstOrDefaultAsync(x=>x.ServiceeId==id);

            if (ServiceeModel==null)
            {
                return null;
                
            }
            _ = _context.Servicees.Remove(ServiceeModel);
            _ = await _context.SaveChangesAsync();
            return ServiceeModel;
        }

        public async Task<List<Servicee>> GetAllAsync(QO1 query)
        {
            var Servicees=_context.Servicees.Include(c=>c.Reviews).ThenInclude(a => a.Customer).AsQueryable();
            if (!string.IsNullOrWhiteSpace(query.Name))
            {
                Servicees = Servicees.Where(s => s.Name.Contains(query.Name));
            }
            
            if (!string.IsNullOrWhiteSpace(query.SortBy))
            {
                if (query.SortBy.Equals("Name",StringComparison.OrdinalIgnoreCase))
                {
                    Servicees=query.IsDecending ? Servicees.OrderByDescending(s=>s.Name):Servicees.OrderBy(s=>s.Name);
                    
                }

   
            }
            var skipNumber=(query.PageNumber-1)*query.PageSize;

            return await Servicees.Skip(skipNumber).Take(query.PageSize).ToListAsync();
            
        }

        public async Task<Servicee?> GetByIdAsync(int id)
        {
            return await _context.Servicees.Include(c=>c.Reviews).FirstOrDefaultAsync(i=>i.ServiceeId==id);
        }

        public async Task<Servicee?> GetByNameAsync(string name)
        {
            return await _context.Servicees.FirstOrDefaultAsync(s => s.Name == name);
        }


        public async Task<Servicee?> UpdateAsync(int id, UpdateServiceeRequestDto updateDto)
        {
            var existingServicee=await _context.Servicees.FirstOrDefaultAsync(x=>x.ServiceeId==id);

            if(existingServicee==null){
                return null;
            }
            existingServicee.Name=updateDto.Name;
            existingServicee.Description=updateDto.Description;
            existingServicee.Price=updateDto.Price;

            _ = await _context.SaveChangesAsync();

            return existingServicee;
        }
    }
}