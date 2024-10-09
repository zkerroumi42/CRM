using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Contact;
using api.models;

namespace api.Mappers
{
    public static class ContactMappers
    {
        public static ContactDto ToContactDto(this Contact ContactModel){
            return new ContactDto{
                ContactId=ContactModel.ContactId,
                Name=ContactModel.Name,
                Email=ContactModel.Email,
                PhoneNumber=ContactModel.PhoneNumber,
                CustomerId=ContactModel.CustomerId

            };
        }
        public static Contact ToContacFromCreate(this CreateContactDto contactDto,int CustomerId){
            return new Contact
            {
                Name=contactDto.Name,
                Email=contactDto.Email,
                PhoneNumber=contactDto.PhoneNumber,
                CustomerId=CustomerId
            };
        }

                public static Contact ToContacFromUpdate(this UpdateContactRequestDto contactDto){
            return new Contact
            {
                Name=contactDto.Name,
                Email=contactDto.Email,
                PhoneNumber=contactDto.PhoneNumber,
            };
        }
        

        
    }
}