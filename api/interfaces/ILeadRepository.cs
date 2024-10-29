using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Helpers;
using api.models;

namespace api.interfaces
{
    public interface ILeadRepository
    {
        Task<List<Lead>>GetAllAsync(QO1 query);
        Task<Lead?>GetByIdAsync(int id);
        Task<Lead>CreateAsync(Lead LeadModel);
        Task<Lead?>UpdateAsync(int id,Lead LeadModel);
        Task<Lead?>DeleteAsync(int id);
        Task<Lead?>GetByStatus(string Status);


    }
}