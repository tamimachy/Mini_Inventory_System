namespace Mini_Inventory_System.Models.DTO
{
    public class CreateProductDto
    {
        public string Name {  get; set; }
        public string Barcode { get; set; }
        public decimal Price { get; set; }
        public decimal StockQty { get; set; }
        public string Category { get; set; }
        public bool Status { get; set; }
    }
    public class UpdateProduct: CreateProductDto { }
}
