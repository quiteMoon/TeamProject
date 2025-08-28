using WebWorker.BLL.Dtos.Category;

namespace WebWorker.BLL.Services.Category
{
    public interface ICategoryService
    {
        Task<ServiceResponse> GetAllAsync();
        Task<ServiceResponse> GetByIdAsync(int id);
        Task<ServiceResponse> CreateAsync(CreateCategoryDto dto);
        Task<ServiceResponse> UpdateAsync(UpdateCategoryDto dto);
        Task<ServiceResponse> DeleteAsync(int id);
    }
}
