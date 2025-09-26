using WebWorker.BLL.Dtos.Product;

namespace WebWorker.BLL.Services.Product
{
    public interface IProductService
    {
        Task<ServiceResponse> CreateAsync(CreateProductDto dto);
        Task<ServiceResponse> UpdateAsync(UpdateProductDto dto);
        Task<ServiceResponse> DeleteAsync(int id);
        Task<ServiceResponse> GetByIdAsync(int id);
        Task<ServiceResponse> GetAllAsync(string? categoryName);
    }
}
