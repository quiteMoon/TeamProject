using WebWorker.Data.Entities.Identity;

namespace WebWorker.BLL.Managers.JwtToken
{
    public interface IJwtTokenManager
    {
        Task<string> CrateJwtTokenAsync(UserEntity user);
    }
}
