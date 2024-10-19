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
        Task<Activity>UpdateAsync(int id,UpdateActivityRequestDto ActivityDto);
        Task<Activity>DeleteAsync(int id);
        Task<List<Activity>> GetByClient(int clientId);
        Task<List<Activity>> GetByLead(int leadId);
        // Task<List<Activity>> GetByProject(int projectId);
        Task<List<Activity>> GetBySalesperson(int userId);
        Task<List<Activity>> GetByDay(DateTime date);
        Task<Activity> UpdateStatus(int ActivityId, string status);
    }
}