namespace WebWorker.BLL.Dtos.Product
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string? Image { get; set; }
        public string[] Ingredients { get; set; } = Array.Empty<string>();
    }
}
