using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.SalaryService;
using api.models;

namespace api.Mappers
{
    public static class SalaryServiceMappers
    {
        public static SalaryServiceDto ToSalaryServiceDto(this SalaryService SalaryServiceModel){
            return new SalaryServiceDto{
                SalaryServiceId=SalaryServiceModel.SalaryServiceId,

            };
        }
        public static SalaryService ToSalaryFromCreate(this CreateSalaryServiceRequestDto SalaryServiceDto){
            return new SalaryService
            {

            };
        }

                public static SalaryService ToSalaryFromUpdate(this UpdateSalaryServiceRequestDto SalaryServiceDto){
            return new SalaryService
            {

            };
        }
        

        
    }
}