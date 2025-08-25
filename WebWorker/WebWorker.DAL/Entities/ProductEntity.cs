namespace WebWorker.DAL.Entities
{
    public class ProductEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string? ImageUrl { get; set; }
        public ICollection<CategoryEntity> Categories { get; set; } = new List<CategoryEntity>();
        public ICollection<IngredientEntity> Ingredients { get; set; } = new List<IngredientEntity>();

    }
}
