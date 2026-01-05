namespace Mini_Inventory_System.Models.DTO
{
    public class CreateCustomerDto
    {
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int LoyaltyPoints {  get; set; }
    }
    public class UpdateCustomerDto : CreateCustomerDto { }
}
