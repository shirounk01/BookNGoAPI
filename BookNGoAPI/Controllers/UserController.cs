using BookNGoAPI.Models;
using BookNGoAPI.Models.DTOs;
using BookNGoAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BookNGoAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IUserService _userService;
        private readonly IProfileService _profileService;
        //private readonly string[] _userRoles;


        public UserController(IConfiguration config, IUserService userService, IProfileService profileService)
        {
            _config = config;
            _userService = userService;
            _profileService = profileService;
            //_userRoles = _config.GetSection("Jwt:Roles").Get<string[]>();
        }

        [HttpPost("Login")]
        public IActionResult Login([FromBody] UserInfo loginRequest)
        {
            var user = _userService.FindByEmail(loginRequest.Email);
            if (user != null && _userService.CheckPassword(loginRequest.Password, user))
            {
                string token = _userService.GenerateToken(user);

                return Ok(token);
            }
            return BadRequest();
        }

        [HttpPost("Register")]
        public IActionResult Register([FromBody] UserInfo registerRequest)
        {
            // add login logic
            var user = _userService.FindByEmail(registerRequest.Email);
            if (user == null)
            {
                var newUser = _userService.CreateUser(registerRequest);
                var token = _userService.GenerateToken(newUser);
                return Ok(token);
            }
            return BadRequest();
        }
        [Authorize]
        [HttpGet("Profile")]
        public IActionResult Profile()
        {
            var history = _profileService.GetProfile();
            return Ok(history);
        }

    }
}
