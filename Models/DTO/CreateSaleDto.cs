using Mini_Inventory_System.Models.Domain;

namespace Mini_Inventory_System.Models.DTO
{ 
    public class SaleDetailDto
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public decimal Quantity { get; set; }
        public decimal Price {  get; set; }
    }
    public class CreateSaleDto
    {
        public int? CustomerId { get; set; }
        public decimal PaidAmount { get; set; }
        public List<SaleDetailDto> SaleDetails { get;set; }
    }
}
