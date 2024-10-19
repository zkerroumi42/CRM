using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Opportunity;
using api.Helpers;
using api.models;

namespace api.interfaces
{
    public interface IOpportunityRepository
    {
        Task<List<Opportunity>>GetAllAsync(QueryObject query);
        Task<Opportunity>GetByIdAsync(int id);
        Task<Opportunity>CreateAsync(Opportunity OpportunityModel);
        Task<Opportunity>UpdateAsync(int id,UpdateOpportunityRequestDto OpportunityDto);
        Task<Opportunity>DeleteAsync(int id);
        Task<List<Opportunity>> GetBySalesperson(int AppUserId);
        Task<List<Opportunity>> GetByClient(int clientId);
        Task<List<Opportunity>> GetByLead(int LeadId);
        Task<Opportunity> UpdateStatus(int opportunityId, string status);
        
    }
}