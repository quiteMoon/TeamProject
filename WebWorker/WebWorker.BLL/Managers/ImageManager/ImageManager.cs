using Microsoft.AspNetCore.Http;

namespace WebWorker.BLL.Managers.ImageManager
{
    public class ImageManager : IImageManager
    {
        public async Task<string?> SaveImageAsync(IFormFile image, string directory)
        {
            if (!Directory.Exists(directory))
                return null;

            var imageName = $"{Guid.NewGuid()}{Path.GetExtension(image.FileName)}";
            var path = Path.Combine(directory, imageName);

            using (var fileStream = new FileStream(path, FileMode.Create))
                await image.CopyToAsync(fileStream);

            return imageName;
        }

        public void DeleteImage(string fileName, string directory)
        {
            var path = Path.Combine(directory, fileName);
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }
    }
}
