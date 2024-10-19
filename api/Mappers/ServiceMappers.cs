using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Servicee;
using api.models;

namespace api.Mappers
{
    public static class ServiceeMappers
    {
        public static ServiceeDto ToServiceeDto(this Servicee ServiceeModel){
            return new ServiceeDto{
                ServiceeId=ServiceeModel.ServiceeId,
                Name=ServiceeModel.Name,
                Description=ServiceeModel.Description,
                Price=ServiceeModel.Price,


            };
        }
        public static Servicee ToServiceeFromCreate(this CreateServiceeRequestDto ServiceeDto){
            return new Servicee
            {
                Name=ServiceeDto.Name,
                Description=ServiceeDto.Description,
                Price=ServiceeDto.Price,
            };
        }

                public static Servicee ToServiceeFromUpdate(this UpdateServiceeRequestDto ServiceeDto){
            return new Servicee
            {
                Name=ServiceeDto.Name,
                Description=ServiceeDto.Description,
                Price=ServiceeDto.Price,
            };
        }
        

        
    }
}