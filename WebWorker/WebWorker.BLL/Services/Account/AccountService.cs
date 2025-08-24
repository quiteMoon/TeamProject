using Microsoft.AspNetCore.Identity;
using System.Net.Http.Headers;
using System.Text.Json;
using WebWorker.BLL.Dtos.Account;
using WebWorker.BLL.Dtos.Account.Google;
using WebWorker.BLL.Managers.ImageManager;
using WebWorker.BLL.Managers.JwtToken;
using WebWorker.BLL.Settings;
using WebWorker.Data.Entities.Identity;

namespace WebWorker.BLL.Services.Account
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly IJwtTokenManager _jwtTokenManager;
        private readonly IImageManager _imageManager;

        public AccountService(UserManager<UserEntity> userManager, IJwtTokenManager jwtTokenManager, IImageManager imageManager)
        {
            _userManager = userManager;
            _jwtTokenManager = jwtTokenManager;
            _imageManager = imageManager;
        }

        public async Task<ServiceResponse> GoogleLoginAsync(GoogleLoginRequestModel model)
        {
            if (string.IsNullOrEmpty(model.Token))
                return ServiceResponse.Error("Google token is required");

            var googleUser = await GetGoogleUserInfoAsync(model.Token);
            if (googleUser == null || !googleUser.EmailVerified)
                return ServiceResponse.Error("Google user info is invalid or email not verified");

            var existingUser = await _userManager.FindByEmailAsync(googleUser.Email);

            if (existingUser == null)
                return await CreateUserFromGoogleAsync(googleUser);

            await AddLoginIfNeededAsync(existingUser, googleUser);

            var token = await _jwtTokenManager.CrateJwtTokenAsync(existingUser);
            return ServiceResponse.Success("Google login successful", token);
        }

        private async Task<GoogleUserInfo?> GetGoogleUserInfoAsync(string token)
        {
            using var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await httpClient.GetAsync("https://www.googleapis.com/oauth2/v3/userinfo");
            if (!response.IsSuccessStatusCode)
                return null;

            var userJson = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<GoogleUserInfo>(userJson, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        private async Task<ServiceResponse> CreateUserFromGoogleAsync(GoogleUserInfo googleUser)
        {
            var user = new UserEntity
            {
                UserName = googleUser.Email,
                Email = googleUser.Email,
                FirstName = googleUser.GivenName,
                LastName = googleUser.FamilyName,
                EmailConfirmed = googleUser.EmailVerified
            };

            var result = await _userManager.CreateAsync(user);
            if (!result.Succeeded)
                return ServiceResponse.Error("Failed to create user from Google login");

            var token = await _jwtTokenManager.CrateJwtTokenAsync(user);
            return ServiceResponse.Success("Google login successful", token);
        }

        private async Task AddLoginIfNeededAsync(UserEntity existingUser, GoogleUserInfo googleUser)
        {
            var userLoginGoogle = await _userManager.FindByLoginAsync("Google", googleUser.Id);
            if (userLoginGoogle == null)
                await _userManager.AddLoginAsync(existingUser, new UserLoginInfo("Google", googleUser.Id, "Google"));
        }

        public async Task<ServiceResponse> LoginAsync(LoginDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null)
                return ServiceResponse.Error("User not found");

            if (!await _userManager.HasPasswordAsync(user))
                return ServiceResponse.Error("User does not have a password set");

            var result = await _userManager.CheckPasswordAsync(user, dto.Password);
            if (!result)
                return ServiceResponse.Error("Invalid password");

            var token = await _jwtTokenManager.CrateJwtTokenAsync(user);
            return ServiceResponse.Success("Login successful", token);
        }

        public async Task<ServiceResponse> RegisterAsync(RegisterDto dto)
        {
            if (await _userManager.FindByEmailAsync(dto.Email) != null)
                return ServiceResponse.Error("User with this email already exists");

            var user = new UserEntity
            {
                UserName = dto.Email,
                Email = dto.Email,
                FirstName = dto.FirstName,
                LastName = dto.LastName
            };

            if (dto.Image != null)
            {
                var fileName = await _imageManager.SaveImageAsync(dto.Image, PathSettings.ImageDirectory);
                if (string.IsNullOrEmpty(fileName))
                    return ServiceResponse.Error("Failed to save user image");

                user.Image = fileName;
            }

            var result = await _userManager.CreateAsync(user, dto.Password);

            return result.Succeeded ? ServiceResponse.Success("User registered successfully", 
                await _jwtTokenManager.CrateJwtTokenAsync(user)) :
                ServiceResponse.Error("Failed to register user");
        }
    }
}
