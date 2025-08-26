using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WebWorker.DAL.Entities;

namespace WebWorker.DAL.Repositories.Product
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateAsync(ProductEntity entity)
        {
            await _context.Products.AddAsync(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity == null)
                return false;

            _context.Products.Remove(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        public IQueryable<ProductEntity> GetAll()
            => _context.Products.Include(p => p.Ingredients).Include(p => p.Categories);

        public async Task<ProductEntity?> GetByIdAsync(int id)
            => await GetByPredicateAsync(p => p.Id == id);

        public async Task<ProductEntity?> GetByNameAsync(string name)
            => await GetByPredicateAsync(p => p.Name == name);

        private async Task<ProductEntity?> GetByPredicateAsync(Expression<Func<ProductEntity, bool>> predicate)
            => await _context.Products.FirstOrDefaultAsync(predicate);

        public IEnumerable<ProductEntity> GetProductsByCategoryName(string categoryName)
            => GetProductListBy<CategoryEntity>(categoryName, p => p.Categories, c => c.Name);

        public IEnumerable<ProductEntity> GetProductsByIngredientName(string ingredientName)
            => GetProductListBy<IngredientEntity>(ingredientName, p => p.Ingredients, i => i.Name);

        private IEnumerable<ProductEntity> GetProductListBy<T>(string name, Func<ProductEntity, IEnumerable<T>> navigationSelector, Func<T, string> nameSelector)
            => GetAll().Where(p => navigationSelector(p).Any(e => nameSelector(e) == name));

        public async Task<bool> UpdateAsync(ProductEntity entity)
        {
            _context.Products.Update(entity);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
