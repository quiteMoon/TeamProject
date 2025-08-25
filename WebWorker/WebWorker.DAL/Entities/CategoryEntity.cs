namespace WebWorker.DAL.Entities
{
    public class CategoryEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? ImageUrl { get; set; }
        public ICollection<ProductEntity> Products { get; set; } = new List<ProductEntity>();
    }
}
