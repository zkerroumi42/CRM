using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Campaign;
using api.Mappers;
using api.models;

namespace api.mappers
{
    public static class CampaignMappers
    {
        public static CampaignDto ToCampaignDto(this Campaign CampaignModel){
            return new CampaignDto{
                CampaignId=CampaignModel.CampaignId,
                Name=CampaignModel.Name,
                Budget=CampaignModel.Budget,
                StartDate=CampaignModel.StartDate,
                EndDate=CampaignModel.EndDate,
                Leads=CampaignModel.Leads.Select(s=>s.ToLeadDto()).ToList()
            };

        }
        public static Campaign ToCampaignFromCreateDto(this CreateCampaignRequestDto CampaignDto){
            return new Campaign{
                Budget=CampaignDto.Budget,
                StartDate=CampaignDto.StartDate,
                EndDate=CampaignDto.EndDate,
            };
        }
        
    }
}