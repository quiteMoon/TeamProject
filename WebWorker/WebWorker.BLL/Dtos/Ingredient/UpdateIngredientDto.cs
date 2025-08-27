using Microsoft.AspNetCore.Http;

namespace WebWorker.BLL.Dtos.Ingredient
{
    public class UpdateIngredientDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public IFormFile? Image { get; set; }
    }
}
