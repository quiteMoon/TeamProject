using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WebWorker.DAL.Entities;

namespace WebWorker.DAL.Repositories.Category
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _context;
        public CategoryRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateAsync(CategoryEntity entity)
        {
            await _context.Categories.AddAsync(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(CategoryEntity entity)
        {
            _context.Categories.Remove(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        public IQueryable<CategoryEntity> GetAll()
            => _context.Categories;

        public async Task<CategoryEntity?> GetByIdAsync(int id)
            => await GetByPredicateAsync(p => p.Id == id);

        public async Task<CategoryEntity?> GetByNameAsync(string name)
            => await GetByPredicateAsync(p => p.Name == name);

        private async Task<CategoryEntity?> GetByPredicateAsync(Expression<Func<CategoryEntity, bool>> predicate)
            => await _context.Categories.FirstOrDefaultAsync(predicate);

        public async Task<bool> UpdateAsync(CategoryEntity entity)
        {
            _context.Categories.Update(entity);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
