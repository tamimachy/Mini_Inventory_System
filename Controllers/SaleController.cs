using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mini_Inventory_System.Data;
using Mini_Inventory_System.Models.Domain;
using Mini_Inventory_System.Models.DTO;

namespace Mini_Inventory_System.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SaleController : ControllerBase
    {
        private readonly InventoryDbContext _dbContext;
        private static SemaphoreSlim _semaphore = new SemaphoreSlim(3);


        public SaleController(InventoryDbContext inventoryDb)
        {
            this._dbContext = inventoryDb;
        }

        // Create Method
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateSaleDto createSaleDto)
        {
            if(!await _semaphore.WaitAsync(0))
            {
                return StatusCode(429, "Too many requests");
            }
            try
            {
                await Task.Delay(3000);
                decimal total = 0;
                var saleDetails = new List<SaleDetail>();
                foreach(var item in createSaleDto.SaleDetails)
                {
                    var product = _dbContext.Products.Find(item.ProductId);
                    if(product.StockQty < item.Quantity)
                    {
                        return BadRequest("Insufficient stock");
                    }
                    product.StockQty -= item.Quantity;
                    total += item.Quantity * item.Price;
                    saleDetails.Add(new SaleDetail { 
                        ProductId = item.ProductId,
                        Quantity = item.Quantity,
                        Price = item.Price,
                    });
                }
                var sale = new Sale
                {
                    SaleDate = DateTime.Now,
                    CustomerId = createSaleDto.CustomerId,
                    TotalAmount = total,
                    PaidAmount = createSaleDto.PaidAmount,
                    DueAmount = total - createSaleDto.PaidAmount,
                    SaleDetails = saleDetails
                };
                _dbContext.Sales.Add(sale);
                await _dbContext.SaveChangesAsync();
                return Ok(sale);
            }
            finally
            {
                _semaphore.Release();
            }
        }

        // Sale Report 
        [HttpGet("report")]
        public async Task<IActionResult> SalesReport(DateTime from, DateTime to)
        {
            var sales = _dbContext.Sales
                .Where(s => s.SaleDate >= from && s.SaleDate <= to);
            return Ok(new
            {
                TotalSale = sales.Count(),
                TotalRevenue = sales.Sum(s => s.TotalAmount),
                Transactions = sales.Count()
            });
        }
    }
}
