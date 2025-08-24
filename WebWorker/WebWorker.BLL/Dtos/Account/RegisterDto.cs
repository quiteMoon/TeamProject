using Microsoft.AspNetCore.Http;

namespace WebWorker.BLL.Dtos.Account
{
    public class RegisterDto
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public IFormFile? Image { get; set; } = null;
    }
}
