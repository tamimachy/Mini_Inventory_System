using Mini_Inventory_System.Models.Domain;

namespace Mini_Inventory_System.Models.DTO
{
    public class SaleDto
    {
        public int SaleId { get; set; }
        public DateTime SaleDate { get; set; }
        public int? CustomerId { get; set; }
        public Customer? Customer { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal PaidAmount { get; set; }
        public decimal DueAmount { get; set; }
        public List<SaleDetail> SaleDetails { get; set; } = new();
    }
}
