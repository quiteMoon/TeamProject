using Microsoft.AspNetCore.Http;

namespace WebWorker.BLL.Managers.ImageManager
{
    public class ImageManager : IImageManager
    {
        public async Task<string?> SaveImageAsync(IFormFile image, string directory)
        {
            if (!Directory.Exists(directory))
                return null;

            var imageName = $"{Guid.NewGuid()}.{Path.GetExtension(image.FileName)}";
            var path = Path.Combine(directory, imageName);

            using (var fileStream = new FileStream(path, FileMode.Create))
                await image.CopyToAsync(fileStream);

            return imageName;
        }
    }
}
