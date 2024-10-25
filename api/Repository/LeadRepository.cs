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
    public class LeadRepository:ILeadRepository
    {
        private readonly ApplicationDBContext _context;
        public LeadRepository(ApplicationDBContext context)
        {
            _context=context;
            
        }

        public async Task<Lead> CreateAsync(Lead LeadModel)
        {
            await _context.Leads.AddAsync(LeadModel);
            await _context.SaveChangesAsync();
            return LeadModel;
        }

        public async Task<Lead?> DeleteAsync(int id)
        {
            var LeadModel=await _context.Leads.FirstOrDefaultAsync(x=>x.LeadId==id);

            if (LeadModel==null)
            {
                return null;
            }
            _context.Leads.Remove(LeadModel);
            await _context.SaveChangesAsync();
            return LeadModel;
        }

        public async Task<List<Lead>> GetAllAsync(QueryObject query)
        {
                        var Leads = _context.Leads.AsQueryable();


            if (!string.IsNullOrWhiteSpace(query.SortBy))
            {
                Leads = query.IsDecending
                    ? Leads.OrderByDescending(a => EF.Property<object>(a, query.SortBy))
                    : Leads.OrderBy(a => EF.Property<object>(a, query.SortBy));
            }
            var skipNumber = (query.PageNumber - 1) * query.PageSize;
            return await Leads.Skip(skipNumber).Take(query.PageSize).ToListAsync();
        }

        public async Task<Lead?> GetByIdAsync(int id)
        {
             return await _context.Leads.FindAsync(id);
        }

        public async Task<Lead?> GetByStatus(string Status)
        {
             return await _context.Leads
                                 .FirstOrDefaultAsync(l => l.Status.Equals(Status, StringComparison.OrdinalIgnoreCase));
        }

        public async Task<Lead?> UpdateAsync(int id, Lead LeadModel)
        {
            var existingLead=await _context.Leads.FindAsync(id);
            if (existingLead==null)
            {
                return null;
            }
            existingLead.Name=LeadModel.Name;
            existingLead.Status=LeadModel.Status;
            existingLead.LeadSource=LeadModel.LeadSource;
            await _context.SaveChangesAsync();
            return existingLead;

        }
    }
}