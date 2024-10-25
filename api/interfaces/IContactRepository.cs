using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Helpers;
using api.models;

namespace api.interfaces
{
    public interface IContactRepository
    {
        Task<List<Contact>>GetAllAsync(QueryObject query);
        Task<Contact?>GetByIdAsync(int id);
        Task<Contact>CreateAsync(Contact ContactModel);
        Task<Contact?>UpdateAsync(int id,Contact contactModel);
        Task<Contact?>DeleteAsync(int id);
        Task<List<Contact>> GetByCustomerId(int customerId);


    }
}