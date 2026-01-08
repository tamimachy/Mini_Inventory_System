using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mini_Inventory_System.Data;
using Mini_Inventory_System.Models.Domain;
using Mini_Inventory_System.Models.DTO;

namespace Mini_Inventory_System.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly InventoryDbContext _dbContext;
        public ProductController(InventoryDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // CREATE Method
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProductDto createProductDto)
        {
            var product = new Product
            {
                Name = createProductDto.Name,
                Barcode = createProductDto.Barcode,
                Price = createProductDto.Price,
                StockQty = createProductDto.StockQty,
                Category = createProductDto.Category,
                Status = createProductDto.Status,
            };
            await _dbContext.Products.AddAsync(product);
            _dbContext.SaveChangesAsync();
            return Ok(product);
        }
        // GET ALL Method
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _dbContext.Products.Where(p=> !p.IsDeleted).ToListAsync());
        }

        // UPDATE Method
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateProductDto updateProductDto)
        {
            var product = await _dbContext.Products.FindAsync(id);
            if (product == null)
                return NotFound();
            product.Name = updateProductDto.Name;
            product.Price = updateProductDto.Price;
            product.StockQty = updateProductDto.StockQty;
            product.Category = updateProductDto.Category;
            product.Status = updateProductDto.Status;

            _dbContext.SaveChangesAsync();
            return Ok(product);
        }

        // DELETE Method
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _dbContext.Products.FindAsync(id);
            if(product == null) 
                return NotFound();
            product.IsDeleted = true;
            _dbContext.SaveChangesAsync();
            return Ok();
        }
    }
}
