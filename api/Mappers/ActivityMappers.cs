using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Activity;
using api.models;

namespace api.Mappers
{
    public static class ActivityMappers
    {
        public static ActivityDto ToActivityDto(this Activity ActivityModel){
            return new ActivityDto{
                ActivityId=ActivityModel.ActivityId,
                Type=ActivityModel.Type,
                Description=ActivityModel.Description,
                Status=ActivityModel.Status,
                Date=ActivityModel.DueDate,

            };
        }
        public static Activity ToActivityFromCreate(this CreateActivityDto ActivityDto,int CustomerId){
            return new Activity
            {
                Type=ActivityDto.Type,
                Description=ActivityDto.Description,
                Status=ActivityDto.Status,
                DueDate=ActivityDto.Date,
                CustomerId=CustomerId
            };
        }

                public static Activity ToActivityFromUpdate(this UpdateActivityRequestDto ActivityDto){
            return new Activity
            {
                Type=ActivityDto.Type,
                Description=ActivityDto.Description,
                Status=ActivityDto.Status,
                DueDate=ActivityDto.Date,
            };
        }
        

        
    }
}