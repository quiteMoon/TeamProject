using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WebWorker.DAL.Entities;

namespace WebWorker.DAL.Repositories.Ingredient
{
    public class IngredientRepository : IIngredientRepository
    {
        private readonly AppDbContext _context;
        public IngredientRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateAsync(IngredientEntity entity)
        {
            await _context.Ingredients.AddAsync(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(IngredientEntity entity)
        {
            _context.Ingredients.Remove(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        public IQueryable<IngredientEntity> GetAll()
            => _context.Ingredients;

        public async Task<IngredientEntity?> GetByIdAsync(int id)
            => await GetByPredicateAsync(p => p.Id == id);

        public async Task<IngredientEntity?> GetByNameAsync(string name)
            => await GetByPredicateAsync(p => p.Name == name);

        private async Task<IngredientEntity?> GetByPredicateAsync(Expression<Func<IngredientEntity, bool>> predicate)
            => await _context.Ingredients.FirstOrDefaultAsync(predicate);

        public async Task<bool> UpdateAsync(IngredientEntity entity)
        {
            _context.Ingredients.Update(entity);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
