using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WebWorker.BLL.Dtos.Product;
using WebWorker.BLL.Managers.ImageManager;
using WebWorker.BLL.Settings;
using WebWorker.DAL.Entities;
using WebWorker.DAL.Repositories.Category;
using WebWorker.DAL.Repositories.Ingredient;
using WebWorker.DAL.Repositories.Product;

namespace WebWorker.BLL.Services.Product
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IImageManager _imageManager;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IIngredientRepository _ingredientRepository;
        public ProductService(IProductRepository productRepository, IImageManager imageManager, ICategoryRepository categoryRepository, IIngredientRepository ingredientRepository)
        {
            _productRepository = productRepository;
            _imageManager = imageManager;
            _categoryRepository = categoryRepository;
            _ingredientRepository = ingredientRepository;
        }

        public async Task<ServiceResponse> CreateAsync(CreateProductDto dto)
        {
            if (await _productRepository.GetByNameAsync(dto.Name) != null)
                return ServiceResponse.Error("Product with the same name already exists");

            if (dto.Categories.Length == 0)
                return ServiceResponse.Error("At least one category must be specified");

            if (dto.Ingredients.Length == 0)
                return ServiceResponse.Error("At least one ingredient must be specified");

            var entity = new ProductEntity
            {
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
            };

            var categories = await CheckPropertiesAsync(dto.Categories, _categoryRepository.GetAll(), c => c.Name);
            if (categories == null)
                return ServiceResponse.Error("One or more specified categories do not exist");

            entity.Categories = categories;

            var ingredients = await CheckPropertiesAsync(dto.Ingredients, _ingredientRepository.GetAll(), i => i.Name);
            if (ingredients == null)
                return ServiceResponse.Error("One or more specified ingredients do not exist");

            entity.Ingredients = ingredients;

            if (dto.Image != null)
            {
                var fileName = await _imageManager.SaveImageAsync(dto.Image, PathSettings.ProductsImages);
                entity.ImageUrl = fileName;
            }

            var result = await _productRepository.CreateAsync(entity);

            if (!result)
                return ServiceResponse.Error("Failed to create product");

            return ServiceResponse.Success("Product created successfully");
        }

        private async Task<List<T>?> CheckPropertiesAsync<T>(IEnumerable<string> requestedNames, IQueryable<T> dbQuery, Expression<Func<T, string>> nameSelector)
        {
            var entities = await dbQuery
                .Where(e => requestedNames.Contains(EF.Property<string>(e, GetPropertyName(nameSelector))))
                .ToListAsync();

            var foundNames = entities.AsQueryable().Select(nameSelector).ToList();
            var missingNames = requestedNames.Except(foundNames, StringComparer.OrdinalIgnoreCase).ToList();

            if (missingNames.Any())
                return null;

            return entities;
        }

        private string GetPropertyName<T>(Expression<Func<T, string>> expression)
        {
            if (expression.Body is MemberExpression member)
                return member.Member.Name;

            throw new ArgumentException("Expression must be a property access.", nameof(expression));
        }

        public async Task<ServiceResponse> DeleteAsync(int id)
        {
            var entity = await _productRepository.GetByIdAsync(id);
            if (entity == null)
                return ServiceResponse.Error("Product not found");

            if (!string.IsNullOrEmpty(entity.ImageUrl))
                _imageManager.DeleteImage(entity.ImageUrl, PathSettings.ProductsImages);

            var result = await _productRepository.DeleteAsync(entity);

            if (!result)
                return ServiceResponse.Error("Failed to delete product");

            return ServiceResponse.Success("Product deleted successfully");
        }

        public async Task<ServiceResponse> GetAllAsync(string? categoryName)
        {
            var query = _productRepository.GetAll();

            if (!string.IsNullOrEmpty(categoryName))
            {
                query = query.Where(p => p.Categories.Any(c => c.Name == categoryName));
            }

            var entities = await query.ToListAsync();

            var dtos = entities.Select(e => new ProductDto
            {
                Id = e.Id,
                Name = e.Name,
                Description = e.Description,
                Price = e.Price,
                Image = e.ImageUrl,
                Ingredients = e.Ingredients.Select(i => i.Name).ToArray(),
            }).ToList();

            return ServiceResponse.Success("Products retrieved successfully", dtos);
        }

        public async Task<ServiceResponse> GetByIdAsync(int id)
        {
            var entity = await _productRepository.GetByIdAsync(id);

            if (entity == null)
                return ServiceResponse.Error("Product not found");

            var dto = new ProductDto
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                Price = entity.Price,
                Image = entity.ImageUrl,
                Ingredients = entity.Ingredients.Select(i => i.Name).ToArray(),
            };

            return ServiceResponse.Success("Product retrieved successfully", dto);
        }

        public async Task<ServiceResponse> UpdateAsync(UpdateProductDto dto)
        {
            var entity = await _productRepository.GetByIdAsync(dto.Id);

            if (entity == null)
                return ServiceResponse.Error("Product not found");

            entity.Name = dto.Name;
            entity.Description = dto.Description;
            entity.Price = dto.Price;

            var incomingCategoryNames = dto.Categories.ToHashSet(StringComparer.OrdinalIgnoreCase);
            var currentCategoryNames = entity.Categories.Select(c => c.Name).ToHashSet(StringComparer.OrdinalIgnoreCase);

            if (!incomingCategoryNames.SetEquals(currentCategoryNames))
            {
                var categoriesFromDb = await _categoryRepository.GetAll()
                    .Where(c => incomingCategoryNames.Contains(c.Name))
                    .ToListAsync();

                var foundNames = categoriesFromDb.Select(c => c.Name).ToHashSet(StringComparer.OrdinalIgnoreCase);
                var missing = incomingCategoryNames.Except(foundNames).ToList();

                if (missing.Any())
                    return ServiceResponse.Error($"Missing categories: {string.Join(", ", missing)}");

                entity.Categories.Clear();
                foreach (var category in categoriesFromDb)
                    entity.Categories.Add(category);
            }

            var incomingIngredientNames = dto.Ingredients.ToHashSet(StringComparer.OrdinalIgnoreCase);
            var currentIngredientNames = entity.Ingredients.Select(i => i.Name).ToHashSet(StringComparer.OrdinalIgnoreCase);

            if (!incomingIngredientNames.SetEquals(currentIngredientNames))
            {
                var ingredientsFromDb = await _ingredientRepository.GetAll()
                    .Where(i => incomingIngredientNames.Contains(i.Name))
                    .ToListAsync();

                var foundNames = ingredientsFromDb.Select(i => i.Name).ToHashSet(StringComparer.OrdinalIgnoreCase);
                var missing = incomingIngredientNames.Except(foundNames).ToList();

                if (missing.Any())
                    return ServiceResponse.Error($"Missing ingredients: {string.Join(", ", missing)}");

                entity.Ingredients.Clear();
                foreach (var ingredient in ingredientsFromDb)
                    entity.Ingredients.Add(ingredient);
            }

            if (dto.Image != null)
            {
                if (!string.IsNullOrEmpty(entity.ImageUrl))
                    _imageManager.DeleteImage(entity.ImageUrl, PathSettings.ProductsImages);
                var fileName = await _imageManager.SaveImageAsync(dto.Image, PathSettings.ProductsImages);
                entity.ImageUrl = fileName;
            }

            if (!await _productRepository.UpdateAsync(entity))
                return ServiceResponse.Error("Failed to update product");

            return ServiceResponse.Success("Productd updated successfully");
        }
    }
}
