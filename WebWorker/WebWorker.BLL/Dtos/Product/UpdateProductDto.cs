using Microsoft.AspNetCore.Http;

namespace WebWorker.BLL.Dtos.Product
{
    public class UpdateProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string[] Categories { get; set; } = Array.Empty<string>();
        public string[] Ingredients { get; set; } = Array.Empty<string>();
        public IFormFile? Image { get; set; }
    }
}
