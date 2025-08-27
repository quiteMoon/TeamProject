using WebWorker.BLL.Dtos.Category;

namespace WebWorker.BLL.Services.Category
{
    public interface ICategoryService
    {
        Task<List<CategoryDto>> GetAllAsync();
        Task<CategoryDto?> GetByIdAsync(int id);
        Task<bool> CreateAsync(CreateCategoryDto dto);
        Task<CategoryDto?> UpdateAsync(UpdateCategoryDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
