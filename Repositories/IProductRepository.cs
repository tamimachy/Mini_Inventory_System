using Mini_Inventory_System.Models.Domain;

namespace Mini_Inventory_System.Repositories
{
    public interface IProductRepository
    {
        Task<Product> CreateProductAsync(Product product);
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<Product?> UpdateProductAsync(int id, Product product);
        Task<Product?> DeleteProductAsync(int id);
    }
}
