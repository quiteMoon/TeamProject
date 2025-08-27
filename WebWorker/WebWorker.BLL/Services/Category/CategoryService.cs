using Microsoft.EntityFrameworkCore;
using WebWorker.DAL.Entities;
using WebWorker.BLL.Dtos.Category;
using WebWorker.DAL.Repositories.Category;

namespace WebWorker.BLL.Services.Category
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<List<CategoryDto>> GetAllAsync()
        {
            return await _categoryRepository.GetAll()
                .Select(c => new CategoryDto
                {
                    Id = c.Id,
                    Name = c.Name
                }).ToListAsync();
        }

        public async Task<CategoryDto?> GetByIdAsync(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null) return null;

            return new CategoryDto
            {
                Id = category.Id,
                Name = category.Name
            };
        }

        public async Task<bool> CreateAsync(CreateCategoryDto dto)
        {
            var entity = new CategoryEntity
            {
                Name = dto.Name
            };

            return await _categoryRepository.CreateAsync(entity);
        }

        public async Task<CategoryDto?> UpdateAsync(UpdateCategoryDto dto)
        {
            var entity = await _categoryRepository.GetByIdAsync(dto.Id);
            if (entity == null) return null;

            entity.Name = dto.Name;
            await _categoryRepository.UpdateAsync(entity);

            return new CategoryDto
            {
                Id = entity.Id,
                Name = entity.Name
            };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _categoryRepository.GetByIdAsync(id);
            if (entity == null) return false;

            return await _categoryRepository.DeleteAsync(entity);
        }
    }
}
