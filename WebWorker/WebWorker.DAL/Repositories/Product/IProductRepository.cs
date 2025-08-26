using WebWorker.DAL.Entities;

namespace WebWorker.DAL.Repositories.Product
{
    public interface IProductRepository
    {
        Task<bool> CreateAsync(ProductEntity entity);
        Task<bool> UpdateAsync(ProductEntity entity);
        Task<bool> DeleteAsync(int id);
        Task<ProductEntity?> GetByIdAsync(int id);
        Task<ProductEntity?> GetByNameAsync(string name);
        IEnumerable<ProductEntity> GetProductsByCategoryName(string categoryName);
        IEnumerable<ProductEntity> GetProductsByIngredientName(string ingredientName);
        IQueryable<ProductEntity> GetAll();
    }
}
