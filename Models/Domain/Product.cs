using Microsoft.EntityFrameworkCore;

namespace Mini_Inventory_System.Models.Domain
{
    [Index(nameof(Barcode), IsUnique = true)]
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Barcode { get; set; }
        public Decimal Price { get; set; }
        public Decimal StockQty { get; set; }
        public string Category { get; set; }
        public bool Status { get; set; }
    }
}
