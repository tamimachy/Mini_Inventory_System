using Microsoft.EntityFrameworkCore;
using Mini_Inventory_System.Data;
using Mini_Inventory_System.Models.Domain;
using Mini_Inventory_System.Models.DTO;

namespace Mini_Inventory_System.Repositories
{
    public class SQLCustomerRepository : ICustomerRepository
    {
        private readonly InventoryDbContext _dbContext;
        public SQLCustomerRepository(InventoryDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Task<Customer> CreateCustomerAsync(CreateCustomerDto createCustomerDto)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteCustomerAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
        {
            return await _dbContext.Customers.ToListAsync();
        }
    }
}
