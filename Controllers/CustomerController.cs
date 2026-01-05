using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public IActionResult Create(CreateCustomerDto createCustomerDto)
        {
            var customer = new Customer
            {
                FullName = createCustomerDto.FullName,
                Phone = createCustomerDto.Phone,
                Email = createCustomerDto.Email,
                LoyaltyPoints = createCustomerDto.LoyaltyPoints
            };
            _dbContext.Customers.Add(customer);
            _dbContext.SaveChanges();
            return Ok(customer);
        }
        
        // GET ALL Method
        public IActionResult GetAll()
        {
            return Ok(_dbContext.Customers.Where(c=>!c.IsDeleted).ToList());
        }

        // DELETE
        [HttpDelete("{id}")]
        public IActionResult Delete(int id) 
        {
            var customer = _dbContext.Customers.Find(id);
            if (customer == null)
                return NotFound();
            customer.IsDeleted = true;
            _dbContext.SaveChanges();
            return Ok();
        }
    }
}
