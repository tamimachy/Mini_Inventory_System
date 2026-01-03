using Mini_Inventory_System.Models.Domain;

namespace Mini_Inventory_System.Models.DTO
{
    public class SaleDetailDto
    {
        public int SaleDetailId { get; set; }
        public int SaleId { get; set; }
        public Sale Sale { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public decimal Quantity { get; set; }
        public decimal Price { get; set; }
    }
}