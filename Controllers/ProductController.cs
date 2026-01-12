using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mini_Inventory_System.Data;
using Mini_Inventory_System.Models.Domain;
using Mini_Inventory_System.Models.DTO;
using Mini_Inventory_System.Repositories;

namespace Mini_Inventory_System.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly InventoryDbContext _dbContext;
        private readonly IProductRepository _productRepository;

        public ProductController(InventoryDbContext dbContext, IProductRepository productRepository)
        {
            _dbContext = dbContext;
            _productRepository = productRepository;
        }

        // CREATE Method
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProductDto createProductDto)
        {
            var productDomain = new Product
            {
                Name = createProductDto.Name,
                Barcode = createProductDto.Barcode,
                Price = createProductDto.Price,
                StockQty = createProductDto.StockQty,
                Category = createProductDto.Category,
                Status = createProductDto.Status,
            };
            // Use Repository to create product
            await _productRepository.CreateProductAsync(productDomain);
            return Ok(productDomain);
        }
        // GET ALL Method
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var product = await _productRepository.GetAllProductsAsync();
            //var allProduct = _dbContext.Products.Where(p=> !p.IsDeleted).ToList();
            return Ok(product);
        }

        // UPDATE Method
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateProductDto updateProductDto)
        {
            // Map DTO to Domain Model
            var productDomainModel = new Product
            {
                Name = updateProductDto.Name,
                Barcode = updateProductDto.Barcode,
                Price = updateProductDto.Price,
                StockQty = updateProductDto.StockQty,
                Category = updateProductDto.Category,
                Status = updateProductDto.Status
            };
            // Use Repository to update product
            var updateProduct = await _productRepository.UpdateProductAsync(id,productDomainModel);
            if (updateProduct == null)
            {
                return NotFound();
            }
            return Ok(updateProduct);
        }

        // DELETE Method
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var productDomain = await _productRepository.DeleteProductAsync(id);
            if(productDomain == null) 
                return NotFound();
            productDomain.IsDeleted = true;
            await _dbContext.SaveChangesAsync();
            return Ok();
        }
    }
}
