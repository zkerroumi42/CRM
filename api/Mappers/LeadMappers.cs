using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Lead;
using api.models;

namespace api.Mappers
{
    public static class LeadMappers
    {
        public static LeadDto ToLeadDto(this Lead LeadModel){
            return new LeadDto{
                LeadId=LeadModel.LeadId,
                Name=LeadModel.Name,
                Status=LeadModel.Status,
                Source=LeadModel.LeadSource,
                CampaignId=LeadModel.CampaignId

            };
        }
        public static Lead ToLeadFromCreate(this CreateLeadDto LeadDto,int campaignId){
            return new Lead
            {
                Name=LeadDto.Name,
                Status=LeadDto.Status,
                LeadSource=LeadDto.Source,
                CampaignId=campaignId
            };
        }

                public static Lead ToLeadFromUpdate(this UpdateLeadRequestDto LeadDto){
            return new Lead
            {
                Name=LeadDto.Name,
                Status=LeadDto.Status,
                LeadSource=LeadDto.Source,
            };
        }
        

        
    }
}