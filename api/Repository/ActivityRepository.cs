using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Activity;
using api.Helpers;
using api.interfaces;
using api.models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class ActivityRepository:IActivityRepository
    {
        private readonly ApplicationDBContext _context;
        public ActivityRepository(ApplicationDBContext context)
        {
            _context=context;
            
        }

        public async Task<Activity> CreateAsync(Activity ActivityModel)
        {
            _ = await _context.Activities.AddAsync(ActivityModel);
            _ = await _context.SaveChangesAsync();
            return ActivityModel;
        }

        public async Task<Activity?> DeleteAsync(int id)
        {
            var ActivityModel=await _context.Activities.FirstOrDefaultAsync(x=>x.ActivityId==id);

            if (ActivityModel==null)
            {
                return null;
            }
            _ = _context.Activities.Remove(ActivityModel);
            _ = await _context.SaveChangesAsync();
            return ActivityModel;
        }

        public async Task<List<Activity>> GetAllAsync(QueryObject query)
        {
            var Activitys = _context.Activities.AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.SortBy))
            {
                Activitys = query.IsDecending
                    ? Activitys.OrderByDescending(c => EF.Property<object>(c, query.SortBy))
                    : Activitys.OrderBy(c => EF.Property<object>(c, query.SortBy));
            }

            var skipNumber = (query.PageNumber - 1) * query.PageSize;
            return await Activitys.Skip(skipNumber).Take(query.PageSize).ToListAsync();
                
                }

        public async Task<List<Activity>> GetByCustomerId(int customerId)
        {
                return await _context.Activities
                         .Where(c => c.CustomerId == customerId) 
                         .ToListAsync();
        }

        public async Task<List<Activity>> GetByDay(DateTime date)
        {
            return await _context.Activities
                         .Where(c => c.DueDate.Date == date.Date) 
                         .ToListAsync();
        }

        public async Task<Activity?> GetByIdAsync(int id)
        {
             return await _context.Activities.FindAsync(id);
        }

        public async Task<List<Activity>> GetByLeadId(int leadId)
        {
            return await _context.Activities
                         .Where(c => c.LeadId == leadId) 
                         .ToListAsync();
        }

        public async Task<List<Activity>> GetBySalespersonId(int salesId)
        {
            return await _context.Activities
                         .Where(c => c.AppUserId == salesId) 
                         .ToListAsync();
        }

        public async Task<Activity?> UpdateAsync(int id, Activity ActivityModel)
        {
            var existingActivity=await _context.Activities.FindAsync(id);
            if (existingActivity==null)
            {
                return null;
            }
            existingActivity.Type=ActivityModel.Type;
            existingActivity.DueDate=ActivityModel.DueDate;
            existingActivity.Status=ActivityModel.Status;
            existingActivity.Description=ActivityModel.Description;
            _ = await _context.SaveChangesAsync();
            return existingActivity;

        }

        public async Task<Activity> UpdateStatus(int ActivityId, string status)
        {
            var activity = await _context.Activities.FindAsync(ActivityId);
            if (activity == null) return null;
            activity.Status = status;
            _ = await _context.SaveChangesAsync();
            return activity;
        }
    }
}