using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mini_Inventory_System.Data;
using Mini_Inventory_System.Models.Domain;

namespace Mini_Inventory_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaleController : ControllerBase
    {
        private readonly InventoryDbContext _inventoryDb;

        public SaleController(InventoryDbContext inventoryDb)
        {
            this._inventoryDb = inventoryDb;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var sales_product = new List<Sale>
            {
                new Sale
                {
                    SaleId = 1,
                    SaleDate = DateTime.Now,
                    TotalAmount = 1500000,
                    PaidAmount = 1500000,
                    DueAmount = 0,

                }
            };
            return Ok(sales_product);
        }
    }
}
