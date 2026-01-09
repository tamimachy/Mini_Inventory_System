using Mini_Inventory_System.Models.Domain;
using Mini_Inventory_System.Models.DTO;

namespace Mini_Inventory_System.Repositories
{
    public interface ICustomerRepository
    {
        Task<Customer> CreateCustomerAsync(CreateCustomerDto createCustomerDto);
        Task<IEnumerable<Customer>> GetAllCustomersAsync();
        Task<bool> DeleteCustomerAsync(int id);
    }
}
