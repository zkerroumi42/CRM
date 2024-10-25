using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Activity;
using api.Helpers;
using api.models;

namespace api.interfaces
{
    public interface IActivityRepository
    {
        Task<List<Activity>>GetAllAsync(QueryObject query);
        Task<Activity>GetByIdAsync(int id);
        Task<Activity>CreateAsync(Activity ActivityModel);
        Task<Activity>UpdateAsync(int id,Activity ActivityDto);
        Task<Activity>DeleteAsync(int id);
        Task<List<Activity>> GetByCustomerId(int customerId);
        Task<List<Activity>> GetByLeadId(int leadId);
        // // Task<List<Activity>> GetByProject(int projectId);
        Task<List<Activity>> GetBySalespersonId(int salesId);
        Task<List<Activity>> GetByDay(DateTime date);
        Task<Activity> UpdateStatus(int ActivityId, string status);
    }
}