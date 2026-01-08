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
    public class CustomerController : ControllerBase
    {
        private readonly InventoryDbContext _dbContext;
        public CustomerController(InventoryDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        //CREATE Method
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCustomerDto createCustomerDto)
        {
            var customer = new Customer
            {
                FullName = createCustomerDto.FullName,
                Phone = createCustomerDto.Phone,
                Email = createCustomerDto.Email,
                LoyaltyPoints = createCustomerDto.LoyaltyPoints
            };
            await _dbContext.Customers.AddAsync(customer);
            _dbContext.SaveChangesAsync();
            return Ok(customer);
        }

        // GET ALL Method
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _dbContext.Customers.Where(c=>!c.IsDeleted).ToListAsync());
        }

        // DELETE
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id) 
        {
            var customer = await _dbContext.Customers.FindAsync(id);
            if (customer == null)
                return NotFound();
            customer.IsDeleted = true;
            _dbContext.SaveChangesAsync();
            return Ok();
        }
    }
}
