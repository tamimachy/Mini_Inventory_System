using Microsoft.EntityFrameworkCore;
using Mini_Inventory_System.Data;
using Mini_Inventory_System.Models.Domain;
using Mini_Inventory_System.Models.DTO;

namespace Mini_Inventory_System.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly InventoryDbContext _dbContext;
        public CustomerRepository(InventoryDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Customer> CreateCustomerAsync(Customer customer)
        {
            await _dbContext.Customers.AddAsync(customer);
            await _dbContext.SaveChangesAsync();
            return customer;
        }

        public async Task<Customer?> DeleteCustomerAsync(int id)
        {
            var existingCustomer = await _dbContext.Customers.FirstOrDefaultAsync(x => x.CustomerId == id);
            if(existingCustomer == null)
            {
                return null;
            }
            _dbContext.Customers.Remove(existingCustomer);
            await _dbContext.SaveChangesAsync();
            return existingCustomer;
        }

        public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
        {
            return await _dbContext.Customers.ToListAsync();
        }

        public async Task<Customer?> UpdateCustomerAsync(int id, Customer customer)
        {
            var existingCustomer = await _dbContext.Customers.FirstOrDefaultAsync(x => x.CustomerId == id);
            if (existingCustomer == null)
            {
                return null;
            }
            existingCustomer.FullName = customer.FullName;
            existingCustomer.Phone = customer.Phone;
            existingCustomer.Email = customer.Email;
            existingCustomer.LoyaltyPoints = customer.LoyaltyPoints;

            await _dbContext.SaveChangesAsync();
            return existingCustomer;
        }
    }
}
