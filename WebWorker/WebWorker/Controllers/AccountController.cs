using Microsoft.AspNetCore.Mvc;
using WebWorker.BLL.Services.Account;
using WebWorker.BLL.Dtos.Account.Google;
using WebWorker.BLL.Dtos.Account;
using WebWorker.BLL.Managers.JwtToken;

namespace WebWorker.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IJwtTokenManager _jwtTokenManager;

        public AccountController(IAccountService accountService, IJwtTokenManager jwtTokenManager)
        {
            _accountService = accountService;
            _jwtTokenManager = jwtTokenManager;
        }

        [HttpPost("google-login")]
        public async Task<IActionResult> GoogleLogin([FromBody] GoogleLoginRequestModel model)
        {
            if (string.IsNullOrEmpty(model.Token))
                return BadRequest("Token is required.");

            var result = await _accountService.GoogleLoginAsync(model);

            return result.IsSuccess ? Ok(result.Message) : BadRequest(result.Message);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {           
            if (string.IsNullOrEmpty(dto.Email) || string.IsNullOrEmpty(dto.Password))
                return BadRequest("Email and Password are required.");

            var result = await _accountService.LoginAsync(dto);

            return result.IsSuccess ? Ok($"{result.Message}\n{result.Payload}") : BadRequest(result.Message);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromForm] RegisterDto dto)
        {
            if (string.IsNullOrEmpty(dto.Email) || string.IsNullOrEmpty(dto.Password))
                return BadRequest("Email and Password are required.");

            var result = await _accountService.RegisterAsync(dto);

            return result.IsSuccess ? Ok($"{result.Message}\n{result.Payload}") : BadRequest(result.Message);
        }
    }
}
