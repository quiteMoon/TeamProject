using Microsoft.EntityFrameworkCore;
using WebWorker.DAL.Entities;
using WebWorker.DAL;
using WebWorker.BLL.Dtos.Category;

namespace WebWorker.BLL.Services.Category
{
    public class CategoryService : ICategoryService
    {
        private readonly AppDbContext _context;

        public CategoryService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CategoryDto>> GetAllAsync()
        {
            return await _context.Categories
                .Select(c => new CategoryDto
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .ToListAsync();
        }

        public async Task<CategoryDto?> GetByIdAsync(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null) return null;

            return new CategoryDto
            {
                Id = category.Id,
                Name = category.Name
            };
        }

        public async Task<CategoryDto> CreateAsync(CreateCategoryDto dto)
        {
            var entity = new CategoryEntity
            {
                Name = dto.Name
            };

            _context.Categories.Add(entity);
            await _context.SaveChangesAsync();

            return new CategoryDto
            {
                Id = entity.Id,
                Name = entity.Name
            };
        }

        public async Task<CategoryDto?> UpdateAsync(UpdateCategoryDto dto)
        {
            var entity = await _context.Categories.FindAsync(dto.Id);
            if (entity == null) return null;

            entity.Name = dto.Name;
            await _context.SaveChangesAsync();

            return new CategoryDto
            {
                Id = entity.Id,
                Name = entity.Name
            };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Categories.FindAsync(id);
            if (entity == null) return false;

            _context.Categories.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
