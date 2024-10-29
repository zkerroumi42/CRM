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
        Task<List<Opportunity>>GetAllAsync(QO1 query);
        Task<Opportunity>GetByIdAsync(int id);
        Task<Opportunity>CreateAsync(Opportunity OpportunityModel);
        Task<Opportunity>UpdateAsync(int id,Opportunity OpportunityDto);
        Task<Opportunity>DeleteAsync(int id);
        Task<List<Opportunity>> GetByCustomerId(int customerId);
        Task<List<Opportunity>> GetByLeadId(int leadId);
        // // Task<List<Opportunity>> GetByProject(int projectId);
        Task<List<Opportunity>> GetBySalespersonId(int salesId);
        Task<List<Opportunity>> GetByDay(DateTime date);
        Task<Opportunity> UpdateStatus(int OpportunityId, string status);
    }
}