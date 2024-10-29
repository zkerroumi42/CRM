using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Servicee;
using api.Helpers;
using api.models;

namespace api.interfaces
{
    public interface IServiceeRepository
    {
        Task<List<Servicee>>GetAllAsync(QO1 query);
        Task<Servicee>GetByIdAsync(int id);
        Task<Servicee>CreateAsync(Servicee ServiceeModel);
        Task<Servicee?> GetByNameAsync(string name);
        Task<Servicee>UpdateAsync(int id,UpdateServiceeRequestDto ServiceeDto);
        Task<Servicee>DeleteAsync(int id);
        Task<bool>ServiceeExists(int id);
        
    }
}