using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Opportunity;
using api.models;

namespace api.Mappers
{
    public static class OpportunityMappers
    {
        public static OpportunityDto ToOpportunityDto(this Opportunity OpportunityModel){
            return new OpportunityDto{
                OpportunityId=OpportunityModel.OpportunityId,
                Name=OpportunityModel.Name,
                Value=OpportunityModel.Value,
                Probability=OpportunityModel.Probability,
                CreatedAt=OpportunityModel.CreatedAt,
                CloseDate=OpportunityModel.CloseDate,
                Status=OpportunityModel.Status

            };
        }
        public static Opportunity ToOpportunityFromCreate(this CreateOpportunityRequestDto OpportunityDto,int CustomerId){
            return new Opportunity
            {
                Name=OpportunityDto.Name,
                Value=OpportunityDto.Value,
                Probability=OpportunityDto.Probability,
                CreatedAt=OpportunityDto.CreatedAt,
                CloseDate=OpportunityDto.CloseDate,
                Status=OpportunityDto.Status
            };
        }

                public static Opportunity ToOpportunityFromUpdate(this UpdateOpportunityRequestDto OpportunityDto){
            return new Opportunity
            {
                Name=OpportunityDto.Name,
                Value=OpportunityDto.Value,
                Probability=OpportunityDto.Probability,
                CreatedAt=OpportunityDto.CreatedAt,
                CloseDate=OpportunityDto.CloseDate,
                Status=OpportunityDto.Status
            };
        }
        

        
    }
}