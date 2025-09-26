using WebWorker.BLL.Dtos.Ingredient;

namespace WebWorker.BLL.Services.Ingredient
{
    public interface IIngredientService
    {
        Task<ServiceResponse> CreateAsync(CreateIngredientDto dto);
        Task<ServiceResponse> UpdateAsync(UpdateIngredientDto dto);
        Task<ServiceResponse> DeleteAsync(int id);
        Task<ServiceResponse> GetByIdAsync(int id);
        Task<ServiceResponse> GetAllAsync();
    }
}
