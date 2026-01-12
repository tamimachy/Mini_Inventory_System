using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mini_Inventory_System.Data;
using Mini_Inventory_System.Models.Domain;
using Mini_Inventory_System.Models.DTO;
using Mini_Inventory_System.Repositories;
using System.Threading.Tasks;

namespace Mini_Inventory_System.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly InventoryDbContext _dbContext;
        private readonly ICustomerRepository _customerRepository;

        public CustomerController(InventoryDbContext dbContext, ICustomerRepository customerRepository)
        {
            _dbContext = dbContext;
            _customerRepository = customerRepository;
        }

        //CREATE Method
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Customer customer)
        {
            var customerDomain = new Customer
            {
                FullName = customer.FullName,
                Phone = customer.Phone,
                Email = customer.Email,
                LoyaltyPoints = customer.LoyaltyPoints
            };

            // Use Repository to create customer
            await _customerRepository.CreateCustomerAsync(customerDomain);
            await _dbContext.SaveChangesAsync();

            return Ok(customer);
        }

        // GET ALL Method
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var customers = await _customerRepository.GetAllCustomersAsync();
            //var allCustomer = _dbContext.Customers.Where(c => !c.IsDeleted).ToList();
            return Ok(customers);
        }

        // DELETE
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id) 
        {
            var customerDomainModel = await _customerRepository.DeleteCustomerAsync(id);
            if (customerDomainModel == null)
                return NotFound();
            customerDomainModel.IsDeleted = true;
            await _dbContext.SaveChangesAsync();
            return Ok();
        }
    }
}
