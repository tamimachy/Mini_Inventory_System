using Mini_Inventory_System.Models.Domain;

namespace Mini_Inventory_System.Repositories
{
    public interface ISaleRepository
    {
        Task<Sale> SaleCreateAsync(Sale sale);
    }
}
