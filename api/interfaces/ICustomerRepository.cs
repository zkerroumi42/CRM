using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Customer;
using api.Helpers;
using api.models;

namespace api.interfaces
{
    public interface ICustomerRepository
    {
        Task<List<Customer>>GetAllAsync(QueryObject query);
        Task<Customer>GetByIdAsync(int id);
        Task<Customer>CreateAsync(Customer CustomerModel);
        Task<Customer>UpdateAsync(int id,UpdateCustomerRequestDto CustomerDto);
        Task<Customer>DeleteAsync(int id);
        Task<bool>CustomerExists(int id);
    }
}