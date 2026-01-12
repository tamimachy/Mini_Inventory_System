using Microsoft.EntityFrameworkCore;
using Mini_Inventory_System.Data;
using Mini_Inventory_System.Models.Domain;
using Mini_Inventory_System.Models.DTO;

namespace Mini_Inventory_System.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly InventoryDbContext _dbContext;
        public ProductRepository(InventoryDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Product> CreateProductAsync(Product product)
        {
            await _dbContext.Products.AddAsync(product);
            await _dbContext.SaveChangesAsync();
            return product;
        }

        public async Task<Product?> DeleteProductAsync(int id)
        {
            var existingProduct = await _dbContext.Products.FirstOrDefaultAsync(x => x.ProductId == id);
            if(existingProduct == null)
            {
                return null;
            }
            _dbContext.Products.Remove(existingProduct);
            await _dbContext.SaveChangesAsync();
            return existingProduct;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _dbContext.Products.ToListAsync();
        }

        public async Task<Product?> UpdateProductAsync(int id, Product product)
        {
            var existingProduct = await _dbContext.Products.FirstOrDefaultAsync(x => x.ProductId == id);
            if (existingProduct == null)
            {
                return null;
            }
            existingProduct.Name = product.Name;
            existingProduct.Barcode = product.Barcode;
            existingProduct.Price = product.Price;
            existingProduct.StockQty = product.StockQty;
            existingProduct.Status = product.Status;
            existingProduct.Category = product.Category;

            await _dbContext.SaveChangesAsync();
            return existingProduct;
        }
    }
}
