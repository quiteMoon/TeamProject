using Microsoft.AspNetCore.Http;

namespace WebWorker.BLL.Managers.ImageManager
{
    public interface IImageManager
    {
        Task<string?> SaveImageAsync(IFormFile image, string directory);
    }
}
