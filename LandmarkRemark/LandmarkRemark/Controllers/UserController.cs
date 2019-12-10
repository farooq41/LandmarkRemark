using System.Threading.Tasks;
using LandmarkRemarkModel;
using LandmarkRemarkService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LandmarkRemark.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // Login: api/User/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Login loginInfo)
        {
            var user = await _userService.GetUser(loginInfo.UserName, loginInfo.Password);
            if (user != null)
                return Ok(user);
            else
                return NotFound("User not found, invalid credentials");
        }

        // Register: api/User/register
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] User user)
        {
            var result = await _userService.CreateUser(user);
            if (string.IsNullOrEmpty(result))
            {
                return CreatedAtRoute("login", new Login() { UserName = user.Username, Password = user.Password });
            }
            return BadRequest(result);
        }
    }
}
