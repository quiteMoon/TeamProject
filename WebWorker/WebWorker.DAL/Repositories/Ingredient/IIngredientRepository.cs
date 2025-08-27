using WebWorker.DAL.Entities;

namespace WebWorker.DAL.Repositories.Ingredient
{
    public interface IIngredientRepository
    {
        Task<bool> CreateAsync(IngredientEntity entity);
        Task<bool> UpdateAsync(IngredientEntity entity);
        Task<bool> DeleteAsync(int id);
        Task<IngredientEntity?> GetByIdAsync(int id);
        Task<IngredientEntity?> GetByNameAsync(string name);
        IQueryable<IngredientEntity> GetAll();
    }
}
