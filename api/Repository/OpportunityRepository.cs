using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Opportunity;
using api.Helpers;
using api.interfaces;
using api.models;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories
{
    public class OpportunityRepository : IOpportunityRepository
    {
        private readonly ApplicationDBContext _context;

        public OpportunityRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<List<Opportunity>> GetAllAsync(QueryObject query)
        {
            var opportunities = _context.Opportunities.AsQueryable();
            
            if (!string.IsNullOrWhiteSpace(query.SortBy))
            {
                opportunities = query.IsDecending
                    ? opportunities.OrderByDescending(o => EF.Property<object>(o, query.SortBy))
                    : opportunities.OrderBy(o => EF.Property<object>(o, query.SortBy));
            }

            var skipNumber = (query.PageNumber - 1) * query.PageSize;
            return await opportunities.Skip(skipNumber).Take(query.PageSize).ToListAsync();
        }

        public async Task<Opportunity> GetByIdAsync(int id)
        {
            return await _context.Opportunities.FindAsync(id);
        }

        public async Task<Opportunity> CreateAsync(Opportunity opportunityModel)
        {
            await _context.Opportunities.AddAsync(opportunityModel);
            await _context.SaveChangesAsync();
            return opportunityModel;
        }

        public async Task<Opportunity> UpdateAsync(int id, UpdateOpportunityRequestDto opportunityDto)
        {
            var opportunity = await _context.Opportunities.FindAsync(id);
            if (opportunity == null) return null;

            opportunity.Name = opportunityDto.Name;
            opportunity.Status = opportunityDto.Status;
            opportunity.Probability = opportunityDto.Probability;
            opportunity.Value = opportunityDto.Value;
            opportunity.CreatedAt = opportunityDto.CreatedAt;
            opportunity.CloseDate = opportunityDto.CloseDate;

            await _context.SaveChangesAsync();
            return opportunity;
        }

        public async Task<Opportunity> DeleteAsync(int id)
        {
            var opportunity = await _context.Opportunities.FindAsync(id);
            if (opportunity == null) return null;

            _context.Opportunities.Remove(opportunity);
            await _context.SaveChangesAsync();
            return opportunity;
        }

        public async Task<List<Opportunity>> GetBySalesperson(int appUserId)
        {
            return await _context.Opportunities
                                 .Where(o => o.AppUserId == appUserId)
                                 .ToListAsync();
        }

        public async Task<List<Opportunity>> GetByClient(int clientId)
        {
            return await _context.Opportunities
                                 .Where(o => o.CustomerId == clientId)
                                 .ToListAsync();
        }

        public async Task<List<Opportunity>> GetByLead(int leadId)
        {
            return await _context.Opportunities
                                 .Where(o => o.LeadId == leadId)
                                 .ToListAsync();
        }

        public async Task<Opportunity> UpdateStatus(int opportunityId, string status)
        {
            var opportunity = await _context.Opportunities.FindAsync(opportunityId);
            if (opportunity == null) return null;

            opportunity.Status = status;
            await _context.SaveChangesAsync();
            return opportunity;
        }
    }
}
