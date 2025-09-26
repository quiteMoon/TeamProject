using Microsoft.EntityFrameworkCore;
using WebWorker.BLL.Dtos.Category;
using WebWorker.BLL.Managers.ImageManager;
using WebWorker.BLL.Settings;
using WebWorker.DAL.Entities;
using WebWorker.DAL.Repositories.Category;

namespace WebWorker.BLL.Services.Category
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IImageManager _imageManager;

        public CategoryService(ICategoryRepository categoryRepository, IImageManager imageManager)
        {
            _categoryRepository = categoryRepository;
            _imageManager = imageManager;
        }

        public async Task<ServiceResponse> GetAllAsync()
        {
            var entities = _categoryRepository.GetAll();

            var result = await entities.Select(c => new CategoryDto
            {
                Id = c.Id,
                Name = c.Name,
                Image = c.ImageUrl
            }).ToListAsync();

            return ServiceResponse.Success("Categories retrieved successfully", result);
        }

        public async Task<ServiceResponse> GetByIdAsync(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null)
                return ServiceResponse.Error("Category not found");

            var dto = new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                Image = category.ImageUrl
            };

            return ServiceResponse.Success("Category retrieved successfully", dto);
        }

        public async Task<ServiceResponse> CreateAsync(CreateCategoryDto dto)
        {
            var entity = new CategoryEntity
            {
                Name = dto.Name
            };

            if (dto.Image != null)
            {
                var fileName = await _imageManager.SaveImageAsync(dto.Image, PathSettings.CategoriesImages);
                entity.ImageUrl = fileName;
            }

            var result = await _categoryRepository.CreateAsync(entity);

            if (!result)
                return ServiceResponse.Error("Failed to create category");

            return ServiceResponse.Success("Category created successfully");
        }

        public async Task<ServiceResponse> UpdateAsync(UpdateCategoryDto dto)
        {
            var entity = await _categoryRepository.GetByIdAsync(dto.Id);
            if (entity == null)
                return ServiceResponse.Error("Category not found");

            entity.Name = dto.Name;

            if (dto.Image != null)
            {
                if (!string.IsNullOrEmpty(entity.ImageUrl))
                    _imageManager.DeleteImage(entity.ImageUrl, PathSettings.CategoriesImages);

                var fileName = await _imageManager.SaveImageAsync(dto.Image, PathSettings.CategoriesImages);
                entity.ImageUrl = fileName;
            }

            if (!await _categoryRepository.UpdateAsync(entity))
                return ServiceResponse.Error("Failed to update category");

            return ServiceResponse.Success("Category updated successfully");
        }

        public async Task<ServiceResponse> DeleteAsync(int id)
        {
            var entity = await _categoryRepository.GetByIdAsync(id);
            if (entity == null)
                return ServiceResponse.Error("Category not found");

            if (!string.IsNullOrEmpty(entity.ImageUrl))
                _imageManager.DeleteImage(entity.ImageUrl, PathSettings.CategoriesImages);

            var result = await _categoryRepository.DeleteAsync(entity);

            if (!result)
                return ServiceResponse.Error("Failed to delete category");

            return ServiceResponse.Success("Category deleted successfully");
        }
    }
}
