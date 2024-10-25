using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Customer;
using api.Mappers;
using api.models;

namespace api.mappers
{
    public static class CustomerMappers
    {
        public static CustomerDto ToCustomerDto(this Customer CustomerModel){
            return new CustomerDto{
                CustomerId=CustomerModel.CustomerId,
                CompanyName=CustomerModel.CompanyName,
                Industry=CustomerModel.Industry,
                Email=CustomerModel.Email,
                PhoneNumber=CustomerModel.PhoneNumber,
                Address=CustomerModel.Address,
                Website=CustomerModel.Website,
                Contacts=CustomerModel.Contacts.Select(s=>s.ToContactDto()).ToList()
            };

        }
        public static Customer ToCustomerFromCreateDto(this CreateCustomerRequestDto CustomerDto){
            return new Customer{
                CompanyName=CustomerDto.CompanyName,
                Industry=CustomerDto.Industry,
                Email=CustomerDto.Email,
                PhoneNumber=CustomerDto.PhoneNumber,
                Address=CustomerDto.Address,
                Website=CustomerDto.Website,
            };
        }
        
    }
}