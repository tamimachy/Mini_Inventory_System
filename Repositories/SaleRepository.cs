using Mini_Inventory_System.Data;
using Mini_Inventory_System.Models.Domain;

namespace Mini_Inventory_System.Repositories
{
    public class SaleRepository : ISaleRepository
    {
        private readonly InventoryDbContext _dbContext;

        public SaleRepository(InventoryDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Sale> SaleCreateAsync(Sale sale)
        {
            await _dbContext.Sales.AddAsync(sale);
            await _dbContext.SaveChangesAsync();
            return sale;
        }
    }
}
