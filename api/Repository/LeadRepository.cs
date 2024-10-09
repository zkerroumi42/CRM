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

        public async Task<List<Lead>> GetAllAsync()
        {
           return await _context.Leads.ToListAsync();
        }

        public async Task<Lead?> GetByIdAsync(int id)
        {
             return await _context.Leads.FindAsync(id);
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
            existingLead.Source=LeadModel.Source;
            await _context.SaveChangesAsync();
            return existingLead;

        }
    }
}