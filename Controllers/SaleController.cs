using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mini_Inventory_System.Data;
using Mini_Inventory_System.Models.Domain;
using Mini_Inventory_System.Models.DTO;
using Mini_Inventory_System.Repositories;

namespace Mini_Inventory_System.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/sales")]
    public class SalesController : ControllerBase
    {
        private readonly InventoryDbContext _dbContext;
        private readonly ISaleRepository _saleRepository;
        private static SemaphoreSlim _semaphore = new SemaphoreSlim(3);

        public SalesController(InventoryDbContext dbContext, ISaleRepository saleRepository)
        {
            _dbContext = dbContext;
            _saleRepository = saleRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateSale(CreateSaleDto dto)
        {
            if (!await _semaphore.WaitAsync(0))
                return StatusCode(429, "Too many requests");

            try
            {
                if(dto.SaleDetails == null || !dto.SaleDetails.Any())
                    return BadRequest("Sale must have at least one item");

                decimal total = 0;
                var saleDetails = new List<SaleDetail>();

                foreach (var item in dto.SaleDetails)
                {
                    var product = await _dbContext.Products.FindAsync(item.ProductId);
                    if(product == null)
                        return BadRequest($"Product with ID {item.ProductId} not found");
                    
                    if (product.StockQty < item.Quantity)
                        return BadRequest("Insufficient stock");

                    product.StockQty -= item.Quantity;
                    total += item.Quantity * item.Price;

                    saleDetails.Add(new SaleDetail
                    {
                        ProductId = item.ProductId,
                        Quantity = item.Quantity,
                        Price = item.Price
                    });
                }

                var sale = new Sale
                {
                    SaleDate = DateTime.Now,
                    CustomerId = dto.CustomerId,
                    TotalAmount = total,
                    PaidAmount = dto.PaidAmount,
                    DueAmount = total - dto.PaidAmount,
                    SaleDetails = saleDetails
                };

                await _saleRepository.SaleCreateAsync(sale);
                return Ok(sale);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
            finally
            {
                _semaphore.Release();
            }
        }

        // Sale Report 
        [HttpGet("report")]
        public IActionResult SalesReport(DateTime from, DateTime to)
        {
            var sales = _dbContext.Sales
                .Where(s => s.SaleDate >= from && s.SaleDate <= to);
            var result = new
            {
                TotalSale = sales.Count(),
                TotalRevenue = sales.Sum(s => s.TotalAmount),
                Transactions = sales.Count()
            }; 
            return Ok(result);
        }
    }
}
