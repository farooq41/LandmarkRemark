using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LandmarkRemarkModel;
using LandmarkRemarkService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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

        // GET: api/User/5
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Login loginInfo)
        {
            var user = await _userService.GetUser(loginInfo.UserName, loginInfo.Password);
            if (user != null)
                return Ok(user);
            else
                return NotFound("User not found, invalid credentials");
        }

        // POST: api/User
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
