namespace Mini_Inventory_System.Models.Domain
{
    public class Sale
    {
        public int SaleId { get; set; }
        public DateTime SaleDate { get; set; }
        public int? CustomerId { get; set; }
        public Customer? Customer { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal PaidAmount { get; set; }
        public decimal DueAmount { get; set; }
        public class SaleDetail
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
}
