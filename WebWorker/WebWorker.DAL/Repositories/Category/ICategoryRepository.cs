using WebWorker.DAL.Entities;

namespace WebWorker.DAL.Repositories.Category
{
    public interface ICategoryRepository
    {
        Task<bool> CreateAsync(CategoryEntity entity);
        Task<bool> UpdateAsync(CategoryEntity entity);
        Task<bool> DeleteAsync(int id);
        Task<CategoryEntity?> GetByIdAsync(int id);
        Task<CategoryEntity?> GetByNameAsync(string name);
        IEnumerable<CategoryEntity> GetAll();
    }
}
