using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Campaign;
using api.Helpers;
using api.interfaces;
using api.models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class CampaignRepository : ICampaignRepository
    {
         private readonly ApplicationDBContext _context;
        public CampaignRepository(ApplicationDBContext context)
        {
            _context=context;
            
        }

        public Task<bool> CampaignExists(int id)
        {
            return _context.Campaigns.AnyAsync(s=>s.CampaignId==id);
        }

        public async Task<Campaign> CreateAsync(Campaign CampaignModel)
        {
            _ = await _context.Campaigns.AddAsync(CampaignModel);
            _ = await _context.SaveChangesAsync();
            return CampaignModel;
        }


        public async Task<Campaign> DeleteAsync(int id)
        {
            var CampaignModel=await _context.Campaigns.FirstOrDefaultAsync(x=>x.CampaignId==id);

            if (CampaignModel==null)
            {
                return null;
                
            }
            _ = _context.Campaigns.Remove(CampaignModel);
            _ = await _context.SaveChangesAsync();
            return CampaignModel;
        }

        public async Task<List<Campaign>> GetAllAsync(QO1 query)
        {
            var Campaigns=_context.Campaigns.Include(c=>c.Leads).AsQueryable();
            if(!string.IsNullOrWhiteSpace(query.Name))
            {
                Campaigns=Campaigns.Where(s=>s.Name.Contains(query.Name));
            }
            if (!string.IsNullOrWhiteSpace(query.SortBy))
            {
                if (query.SortBy.Equals("Name",StringComparison.OrdinalIgnoreCase))
                {
                    Campaigns=query.IsDecending ? Campaigns.OrderByDescending(s=>s.Name):Campaigns.OrderBy(s=>s.Name);
                    
                }

   
            }
            var skipNumber=(query.PageNumber-1)*query.PageSize;

            return await Campaigns.Skip(skipNumber).Take(query.PageSize).ToListAsync();
            
        }

        public async Task<Campaign?> GetByIdAsync(int id)
        {
            return await _context.Campaigns.Include(c=>c.Leads).FirstOrDefaultAsync(i=>i.CampaignId==id);
        }

        public async Task<Campaign?> UpdateAsync(int id, UpdateCampaignRequestDto updateDto)
        {
            var existingCampaign=await _context.Campaigns.FirstOrDefaultAsync(x=>x.CampaignId==id);

            if(existingCampaign==null){
                return null;
            }
            existingCampaign.Name=updateDto.Name;
            existingCampaign.Budget=updateDto.Budget;
            existingCampaign.Description=updateDto.Description;
            existingCampaign.StartDate=updateDto.StartDate;
            existingCampaign.EndDate=updateDto.EndDate;


            _ = await _context.SaveChangesAsync();

            return existingCampaign;
        }
    }
}