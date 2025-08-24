using Microsoft.AspNetCore.Mvc;
using WebWorker.BLL.Services.User;

namespace WebWorker.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("list")]
        public IActionResult GetAll()
        {
            var result = _userService.GetAll();
            return Ok(result);
        }
    }
}
