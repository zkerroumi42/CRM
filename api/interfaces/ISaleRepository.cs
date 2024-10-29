using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Sale;
using api.Helpers;
using api.models;

namespace api.interfaces
{
    public interface ISaleRepository
    {
        Task<List<Sale>>GetAllAsync(QO2 query);
        Task<Sale>GetByIdAsync(int id);
        Task<Sale>CreateAsync(Sale SaleModel);
        Task<Sale>UpdateAsync(int id,UpdateSaleRequestDto SaleDto);
        Task<Sale>DeleteAsync(int id);
        Task<List<Sale>> GetByProject(int projectId);
        Task<List<Sale>> GetBySalesperson(int salespersonId);
        
    }
}