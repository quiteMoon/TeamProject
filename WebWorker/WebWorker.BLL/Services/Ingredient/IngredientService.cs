using Microsoft.EntityFrameworkCore;
using WebWorker.BLL.Dtos.Ingredient;
using WebWorker.BLL.Managers.ImageManager;
using WebWorker.BLL.Settings;
using WebWorker.DAL.Entities;
using WebWorker.DAL.Repositories.Ingredient;

namespace WebWorker.BLL.Services.Ingredient
{
    public class IngredientService : IIngredientService
    {
        private readonly IIngredientRepository _ingredientRepository;
        private readonly IImageManager _imageManager;

        public IngredientService(IIngredientRepository ingredientRepository, IImageManager imageManager)
        {
            _ingredientRepository = ingredientRepository;
            _imageManager = imageManager;
        }

        public async Task<ServiceResponse> CreateAsync(CreateIngredientDto dto)
        {
            if (await _ingredientRepository.GetByNameAsync(dto.Name) != null)
                return ServiceResponse.Error("Ingredient with the same name already exists");

            var entity = new IngredientEntity
            {
                Name = dto.Name
            };

            if (dto.Image != null)
            {
                var fileName = await _imageManager.SaveImageAsync(dto.Image, PathSettings.IngredientsImages);
                entity.ImageUrl = fileName;
            }

            var result = await _ingredientRepository.CreateAsync(entity);

            if (!result)
                return ServiceResponse.Error("Failed to create ingredient");

            return ServiceResponse.Success("Ingredient created successfully");
        }

        public async Task<ServiceResponse> DeleteAsync(int id)
        {
            var entity = await _ingredientRepository.GetByIdAsync(id);

            if (entity == null)
                return ServiceResponse.Error("Ingredient not found");

            if (!string.IsNullOrEmpty(entity.ImageUrl))
                _imageManager.DeleteImage(entity.ImageUrl, PathSettings.IngredientsImages);

            var result = await _ingredientRepository.DeleteAsync(entity);

            if (!result)
                return ServiceResponse.Error("Failed to delete ingredient");

            return ServiceResponse.Success("Ingredient deleted successfully");
        }

        public async Task<ServiceResponse> GetAllAsync()
        {
            var entities = _ingredientRepository.GetAll();

            var result = await entities.Select(e => new IngredientDto
            { 
                Id = e.Id,
                Name = e.Name,
                Image = e.ImageUrl
            }).ToListAsync();

            return ServiceResponse.Success("Ingredients retrieved successfully", result);
        }

        public async Task<ServiceResponse> GetByIdAsync(int id)
        {
            var entity = await _ingredientRepository.GetByIdAsync(id);

            if (entity == null)
                return ServiceResponse.Error("Ingredient not found");

            var dto = new IngredientDto
            {
                Id = entity.Id,
                Name = entity.Name,
                Image = entity.ImageUrl
            };

            return ServiceResponse.Success("Ingredient retrieved successfully", dto);
        }

        public async Task<ServiceResponse> UpdateAsync(UpdateIngredientDto dto)
        {
            var entity = await _ingredientRepository.GetByIdAsync(dto.Id);

            if (entity == null)
                return ServiceResponse.Error("Ingredient not found");

            entity.Name = dto.Name;

            if (dto.Image != null)
            {
                if (!string.IsNullOrEmpty(entity.ImageUrl))
                    _imageManager.DeleteImage(entity.ImageUrl, PathSettings.IngredientsImages);
                var fileName = await _imageManager.SaveImageAsync(dto.Image, PathSettings.IngredientsImages);
                entity.ImageUrl = fileName;
            }

            if (!await _ingredientRepository.UpdateAsync(entity))
                return ServiceResponse.Error("Failed to update ingredient");

            return ServiceResponse.Success("Ingredient updated successfully");
        }
    }
}
