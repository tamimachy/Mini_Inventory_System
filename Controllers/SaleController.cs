using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mini_Inventory_System.Data;
using Mini_Inventory_System.Models.Domain;
using Mini_Inventory_System.Models.DTO;

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

        // GET ALL Sales Data
        [HttpGet]
        public IActionResult GetAll()
        {
            // Get Data from database - domain model
            var salesDomain = _inventoryDb.Sales.ToList();

            // Map Domain Models to DTOs
            var saleDto = new List<SaleDto> ();
            foreach(var sale in salesDomain)
            {
                saleDto.Add(new SaleDto()
                {
                    SaleId = sale.SaleId,
                    SaleDate = sale.SaleDate,
                    CustomerId = sale.CustomerId,
                    Customer = sale.Customer,
                    TotalAmount = sale.TotalAmount,
                    PaidAmount = sale.PaidAmount,
                    DueAmount = sale.DueAmount,
                    SaleDetails = sale.SaleDetailDto
                });
            }
            // Return DTOs
            return Ok(saleDto);
        }

        // GET SINGLE Sale Data
        [HttpGet]
        [Route("{id:int}")]
        public IActionResult GetById([FromRoute] int id)
        {
            // Get Region Domain Model to Database
            var saleDomain = _inventoryDb.Sales.FirstOrDefault(x=>x.SaleId == id);
            if(saleDomain == null)
            {
                return NotFound();
            }
            // Map/Convert Sale Domain Model to Sale DTO 
            var saleDto = new SaleDto
            {
                SaleId = saleDomain.SaleId,
                SaleDate = saleDomain.SaleDate,
                CustomerId = saleDomain.CustomerId,
                Customer = saleDomain.Customer,
                TotalAmount = saleDomain.TotalAmount,
                PaidAmount = saleDomain.PaidAmount,
                DueAmount = saleDomain.DueAmount,
                SaleDetails = saleDomain.SaleDetailDto
            };
            return Ok(saleDto);
        }

        // POST Method to create new Sale Data
        [HttpPost]
        public IActionResult Create([FromBody] AddSaleRequestDto addSaleRequest)
        {
            // Map or Convert DTO to Domain Model
            var saleDomainModel = new Sale
            {
                SaleDate = addSaleRequest.SaleDate,
                CustomerId = addSaleRequest.CustomerId,
                TotalAmount = addSaleRequest.TotalAmount,
                PaidAmount = addSaleRequest.PaidAmount,
                DueAmount = addSaleRequest.DueAmount,
            };

            // Use Domain Model to create Sale
            _inventoryDb.Sales.Add(saleDomainModel);
            _inventoryDb.SaveChanges();

            // Map Domain Model to back to Dto          
            var saleDto = new SaleDto
            {
                SaleId = saleDomainModel.SaleId,
                SaleDate = saleDomainModel.SaleDate,
                CustomerId = saleDomainModel.CustomerId,
                Customer = saleDomainModel.Customer,
                TotalAmount = saleDomainModel.TotalAmount,
                PaidAmount = saleDomainModel.PaidAmount,
                DueAmount = saleDomainModel.DueAmount,
                SaleDetails = saleDomainModel.SaleDetailDto
            };
            return CreatedAtAction(nameof(GetById), new { id = saleDto.SaleId }, saleDto);
        }
    }
}
