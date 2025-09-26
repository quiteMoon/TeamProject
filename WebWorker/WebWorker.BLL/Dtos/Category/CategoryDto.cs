using Microsoft.AspNetCore.Http;

namespace WebWorker.BLL.Dtos.Category
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Image { get; set; }   // посилання / назва картинки
    }

    public class CreateCategoryDto
    {
        public string Name { get; set; } = null!;
        public IFormFile? Image { get; set; }   // файл картинки
    }

    public class UpdateCategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public IFormFile? Image { get; set; }   // файл картинки
    }
}
