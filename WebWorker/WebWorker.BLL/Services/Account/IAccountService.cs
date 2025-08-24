using WebWorker.BLL.Dtos.Account;
using WebWorker.BLL.Dtos.Account.Google;

namespace WebWorker.BLL.Services.Account
{
    public interface IAccountService
    {
        Task<ServiceResponse> GoogleLoginAsync(GoogleLoginRequestModel dto);
        Task<ServiceResponse> RegisterAsync(RegisterDto dto);
        Task<ServiceResponse> LoginAsync(LoginDto dto);
    }
}
