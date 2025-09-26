using Microsoft.AspNetCore.Http;

namespace WebWorker.BLL.Dtos.Ingredient
{
    public class CreateIngredientDto
    {
        public string Name { get; set; } = string.Empty;
        public IFormFile? Image { get; set; }
    }
}
