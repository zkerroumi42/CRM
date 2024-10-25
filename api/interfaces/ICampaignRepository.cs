using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Campaign;
using api.Helpers;
using api.models;

namespace api.interfaces
{
    public interface ICampaignRepository
    {
        
        Task<List<Campaign>>GetAllAsync(QueryObject query);
        Task<Campaign>GetByIdAsync(int id);
        Task<Campaign>CreateAsync(Campaign CampaignModel);
        Task<Campaign>UpdateAsync(int id,UpdateCampaignRequestDto CampaignDto);
        Task<Campaign>DeleteAsync(int id);
        Task<bool>CampaignExists(int id);
        
    }
}