using WebWorker.BLL.Dtos.Ingredient;

namespace WebWorker.BLL.Services.Ingredient
{
    public interface IIngredientService
    {
        Task<ServiceResponse> GetAllAsync();
        Task<ServiceResponse> CreateAsync(CreateIngredientDto dto);
        Task<ServiceResponse> UpdateAsync(UpdateIngredientDto dto);
        Task<ServiceResponse> GetByIdAsync(int id);
        Task<ServiceResponse> DeleteAsync(int id);
    }
}
