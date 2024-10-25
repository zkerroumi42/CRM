using System.Collections.Generic;
using System.Threading.Tasks;
using api.Dtos.Sale;
using api.Helpers;
using api.interfaces;
using api.Mappers;
using api.models;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SaleController : ControllerBase
    {
        private readonly ISaleRepository _saleRepository;

        public SaleController(ISaleRepository saleRepository)
        {
            _saleRepository = saleRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] QueryObject query)
        {
            var sales = await _saleRepository.GetAllAsync(query);
            var saleDtos = sales.Select(s => s.ToSaleDto());
            return Ok(saleDtos);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var sale = await _saleRepository.GetByIdAsync(id);
            if (sale == null)
            {
                return NotFound("Sale not found");
            }

            return Ok(sale.ToSaleDto());
        }

[HttpPost]
public async Task<IActionResult> Create([FromBody] CreateSaleRequestDto saleDto)
{
    if (!ModelState.IsValid)
    {
        return BadRequest(ModelState);
    }

    var saleModel = saleDto.ToSaleFromCreate(); 
    await _saleRepository.CreateAsync(saleModel);
    return CreatedAtAction(nameof(GetById), new { id = saleModel.SaleId }, saleModel.ToSaleDto());
}


        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateSaleRequestDto updateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updatedSale = await _saleRepository.UpdateAsync(id, updateDto);
            if (updatedSale == null)
            {
                return NotFound("Sale not found");
            }

            return Ok(updatedSale.ToSaleDto());
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var sale = await _saleRepository.DeleteAsync(id);
            if (sale == null)
            {
                return NotFound("Sale not found");
            }

            return Ok(sale.ToSaleDto());
        }

        [HttpGet("project/{projectId:int}")]
        public async Task<IActionResult> GetByProject(int projectId)
        {
            var sales = await _saleRepository.GetByProject(projectId);
            if (sales == null || sales.Count == 0)
            {
                return NotFound($"No sales found for project with ID {projectId}.");
            }

            return Ok(sales.Select(s => s.ToSaleDto()));
        }

        [HttpGet("salesperson/{salespersonId:int}")]
        public async Task<IActionResult> GetBySalesperson(int salespersonId)
        {
            var sales = await _saleRepository.GetBySalesperson(salespersonId);
            if (sales == null || sales.Count == 0)
            {
                return NotFound($"No sales found for salesperson with ID {salespersonId}.");
            }

            return Ok(sales.Select(s => s.ToSaleDto()));
        }
    }
}
