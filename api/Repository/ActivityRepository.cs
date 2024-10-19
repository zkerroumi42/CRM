using api.Data;
using api.Dtos.Activity;
using api.Helpers;
using api.interfaces;
using Microsoft.EntityFrameworkCore;
using api.models;
namespace api.Repositories
{
    public class ActivityRepository : IActivityRepository
    {
        private readonly ApplicationDBContext _context;

        public ActivityRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<List<Activity>> GetAllAsync(QueryObject query)
        {
            var activities = _context.Activities.AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.SortBy))
            {
                activities = query.IsDecending
                    ? activities.OrderByDescending(a => EF.Property<object>(a, query.SortBy))
                    : activities.OrderBy(a => EF.Property<object>(a, query.SortBy));
            }

            var skipNumber = (query.PageNumber - 1) * query.PageSize;
            return await activities.Skip(skipNumber).Take(query.PageSize).ToListAsync();
        }

        public async Task<Activity> GetByIdAsync(int id)
        {
            return await _context.Activities.FindAsync(id);
        }

        public async Task<Activity> CreateAsync(Activity activityModel)
        {
            _ = await _context.Activities.AddAsync(activityModel);
            _ = await _context.SaveChangesAsync();
            return activityModel;
        }

        public async Task<Activity> UpdateAsync(int id, UpdateActivityRequestDto activityDto)
        {
            var activity = await _context.Activities.FindAsync(id);
            if (activity == null) return null;

            activity.Type = activityDto.Type;
            activity.Description = activityDto.Description;
            activity.DueDate = activityDto.Date;
            activity.Status = activityDto.Status;

            _ = await _context.SaveChangesAsync();
            return activity;
        }

        public async Task<Activity> DeleteAsync(int id)
        {
            var activity = await _context.Activities.FindAsync(id);
            if (activity == null) return null;

            _ = _context.Activities.Remove(activity);
            _ = await _context.SaveChangesAsync();
            return activity;
        }

        public async Task<List<Activity>> GetByClient(int customerId)
        {
            return await _context.Activities
                                 .Where(a => a.CustomerId == customerId)
                                 .ToListAsync();
        }

        public async Task<List<Activity>> GetByLead(int leadId)
        {
            return await _context.Activities
                                 .Where(a => a.LeadId == leadId)
                                 .ToListAsync();
        }

        public async Task<List<Activity>> GetBySalesperson(int salespersonId)
        {
            return await _context.Activities
                                 .Where(a => a.AppUserId == salespersonId)
                                 .ToListAsync();
        }

        public async Task<List<Activity>> GetByDay(DateTime date)
        {
            return await _context.Activities
                                 .Where(a => a.DueDate.Date == date.Date)
                                 .ToListAsync();
        }

        public async Task<Activity> UpdateStatus(int activityId, string status)
        {
            var activity = await _context.Activities.FindAsync(activityId);
            if (activity == null) return null;

            activity.Status = status;
            _ = await _context.SaveChangesAsync();
            return activity;
        }
    }
}
