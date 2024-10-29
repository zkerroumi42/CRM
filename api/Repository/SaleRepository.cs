using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Sale;
using api.Helpers;
using api.interfaces;
using api.models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class SaleRepository : ISaleRepository
    {
        private readonly ApplicationDBContext _context;

        public SaleRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<List<Sale>> GetAllAsync(QO2 query)
        {
            var sales = _context.Sales.AsQueryable();

            // if(!string.IsNullOrWhiteSpace(query.date))
            // {
            //     sales=sales.Where(s=>s.Date.Contains(query.date));
            // }
            if (!string.IsNullOrWhiteSpace(query.SortBy))
            {
                if (query.SortBy.Equals("CreateAt",StringComparison.OrdinalIgnoreCase))
                {
                    sales=query.IsDecending ? sales.OrderByDescending(s=>s.CreateAt):sales.OrderBy(s=>s.CreateAt);
                    
                }

   
            }
            var skipNumber=(query.PageNumber-1)*query.PageSize;

            return await sales.Skip(skipNumber).Take(query.PageSize).ToListAsync();
            
        }
        public async Task<Sale> GetByIdAsync(int id)
        {
            return await _context.Sales.FindAsync(id);
        }

        public async Task<Sale> CreateAsync(Sale saleModel)
        {
            _ = await _context.Sales.AddAsync(saleModel);
            _ = await _context.SaveChangesAsync();
            return saleModel;
        }

        public async Task<Sale> UpdateAsync(int id, UpdateSaleRequestDto saleDto)
        {
            var sale = await _context.Sales.FindAsync(id);
            if (sale == null) return null;

            sale.Amount = saleDto.Amount;
            sale.CreateAt = saleDto.Date;

            _ = await _context.SaveChangesAsync();
            return sale;
        }

        public async Task<Sale> DeleteAsync(int id)
        {
            var sale = await _context.Sales.FindAsync(id);
            if (sale == null) return null;

            _ = _context.Sales.Remove(sale);
            _ = await _context.SaveChangesAsync();
            return sale;
        }

        public async Task<List<Sale>> GetByProject(int projectId)
        {
            return await _context.Sales
                                 .Where(s => s.ProjectId == projectId)
                                 .ToListAsync();
        }

        public async Task<List<Sale>> GetBySalesperson(int salespersonId)
        {
            return await _context.Sales
                                 .Where(s => s.AppUserId == salespersonId)
                                 .ToListAsync();
        }
    }
}
