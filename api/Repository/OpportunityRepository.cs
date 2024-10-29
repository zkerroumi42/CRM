using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Opportunity;
using api.Helpers;
using api.interfaces;
using api.models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class OpportunityRepository:IOpportunityRepository
    {
        private readonly ApplicationDBContext _context;
        public OpportunityRepository(ApplicationDBContext context)
        {
            _context=context;
            
        }

        public async Task<Opportunity> CreateAsync(Opportunity OpportunityModel)
        {
            _ = await _context.Opportunities.AddAsync(OpportunityModel);
            _ = await _context.SaveChangesAsync();
            return OpportunityModel;
        }

        public async Task<Opportunity?> DeleteAsync(int id)
        {
            var OpportunityModel=await _context.Opportunities.FirstOrDefaultAsync(x=>x.OpportunityId==id);

            if (OpportunityModel==null)
            {
                return null;
            }
            _ = _context.Opportunities.Remove(OpportunityModel);
            _ = await _context.SaveChangesAsync();
            return OpportunityModel;
        }

        public async Task<List<Opportunity>> GetAllAsync(QO1 query)
        {
            var Opportunities = _context.Opportunities.AsQueryable();

            if(!string.IsNullOrWhiteSpace(query.Name))
            {
                Opportunities=Opportunities.Where(s=>s.Name.Contains(query.Name));
            }
            if (!string.IsNullOrWhiteSpace(query.SortBy))
            {
                if (query.SortBy.Equals("Name",StringComparison.OrdinalIgnoreCase))
                {
                    Opportunities=query.IsDecending ? Opportunities.OrderByDescending(s=>s.Name):Opportunities.OrderBy(s=>s.Name);
                    
                }

   
            }
            var skipNumber=(query.PageNumber-1)*query.PageSize;

            return await Opportunities.Skip(skipNumber).Take(query.PageSize).ToListAsync();
            
        }

        public async Task<List<Opportunity>> GetByCustomerId(int customerId)
        {
                return await _context.Opportunities
                         .Where(c => c.CustomerId == customerId) 
                         .ToListAsync();
        }

        public async Task<List<Opportunity>> GetByDay(DateTime date)
        {
            return await _context.Opportunities
                         .Where(c => c.CreatedAt.Date == date.Date) 
                         .ToListAsync();
        }

        public async Task<Opportunity?> GetByIdAsync(int id)
        {
             return await _context.Opportunities.FindAsync(id);
        }

        public async Task<List<Opportunity>> GetByLeadId(int leadId)
        {
            return await _context.Opportunities
                         .Where(c => c.LeadId == leadId) 
                         .ToListAsync();
        }

        public async Task<List<Opportunity>> GetBySalespersonId(int salesId)
        {
            return await _context.Opportunities
                         .Where(c => c.AppUserId == salesId) 
                         .ToListAsync();
        }

        public async Task<Opportunity?> UpdateAsync(int id, Opportunity OpportunityModel)
        {
            var existingOpportunity=await _context.Opportunities.FindAsync(id);
            if (existingOpportunity==null)
            {
                return null;
            }
            existingOpportunity.Name=OpportunityModel.Name;
            existingOpportunity.Value=OpportunityModel.Value;
            existingOpportunity.Status=OpportunityModel.Status;
            existingOpportunity.Probability=OpportunityModel.Probability;
            existingOpportunity.CreatedAt=OpportunityModel.CreatedAt;
            existingOpportunity.CloseDate=OpportunityModel.CloseDate;
            _ = await _context.SaveChangesAsync();
            return existingOpportunity;

        }

        public async Task<Opportunity> UpdateStatus(int OpportunityId, string status)
        {
            var Opportunity = await _context.Opportunities.FindAsync(OpportunityId);
            if (Opportunity == null) return null;
            Opportunity.Status = status;
            _ = await _context.SaveChangesAsync();
            return Opportunity;
        }
    }
}