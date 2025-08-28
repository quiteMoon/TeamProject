namespace WebWorker.BLL.Settings
{
    public static class PathSettings
    {
        public static string ImageDirectory = Path.Combine(Directory.GetCurrentDirectory(), "..", "WebWorker", "images");
        public static string UsersImages => Path.Combine(ImageDirectory, "users");
        public static string IngredientsImages => Path.Combine(ImageDirectory, "ingredients");
        public static string CategoriesImages => Path.Combine(ImageDirectory, "categories");
        public static string ProductsImages => Path.Combine(ImageDirectory, "products");

    }
}
