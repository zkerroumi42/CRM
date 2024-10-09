using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.models;

namespace api.interfaces
{
    public interface ILeadRepository
    {
        Task<List<Lead>>GetAllAsync();
        Task<Lead?>GetByIdAsync(int id);
        Task<Lead>CreateAsync(Lead LeadModel);
        Task<Lead?>UpdateAsync(int id,Lead LeadModel);
        Task<Lead?>DeleteAsync(int id);


    }
}