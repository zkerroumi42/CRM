using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Sale;
using api.models;

namespace api.Mappers
{
    public static class SaleMappers
    {
        public static SaleDto ToSaleDto(this Sale SaleModel){
            return new SaleDto{
                SaleId=SaleModel.SaleId,
                Amount=SaleModel.Amount,
                Date=SaleModel.CreateAt,
            };
        }
        public static Sale ToSaleFromCreate(this CreateSaleRequestDto SaleDto){
            return new Sale
            {
                Amount=SaleDto.Amount,
                CreateAt=SaleDto.Date,

            };
        }

                public static Sale ToSaleFromUpdate(this UpdateSaleRequestDto SaleDto){
            return new Sale
            {
                Amount=SaleDto.Amount,
                CreateAt=SaleDto.Date,
            };
        }
        

        
    }
}