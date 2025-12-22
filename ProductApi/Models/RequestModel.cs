namespace ProductApi.Models
{
    public class ProductCreateRequest
    {
        public string ProductName { get; set; } = null!;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }

    public class ProductUpdateRequest
    {
        public string? ProductName { get; set; }
        public decimal? Price { get; set; }
        public int? Quantity { get; set; }
    }
}
