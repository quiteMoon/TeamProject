using Microsoft.AspNetCore.Hosting;

namespace WebWorker.BLL.Settings
{
    public static class PathSettings
    {
        private static IWebHostEnvironment _env;

        public static void Init(IWebHostEnvironment env)
        {
            _env = env;
        }

        public static string ImageDirectory => Path.Combine(_env.ContentRootPath, "images");
        public static string UsersImages => Path.Combine(ImageDirectory, "users");
        public static string IngredientsImages => Path.Combine(ImageDirectory, "ingredients");
        public static string CategoriesImages => Path.Combine(ImageDirectory, "categories");
        public static string ProductsImages => Path.Combine(ImageDirectory, "products");
    }
}
