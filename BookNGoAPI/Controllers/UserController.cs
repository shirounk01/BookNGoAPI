using BookNGoAPI.Models;
using BookNGoAPI.Models.DTOs;
using BookNGoAPI.Services.Interfaces;
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
        //private readonly string[] _userRoles;


        public UserController(IConfiguration config, IUserService userService)
        {
            _config = config;
            _userService = userService;
            //_userRoles = _config.GetSection("Jwt:Roles").Get<string[]>();
        }

        [HttpPost("Login")]
        public IActionResult Login([FromBody] UserInfo loginRequest)
        {
            var user = _userService.FindByEmail(loginRequest.Email);
            if (user != null && _userService.CheckPassword(loginRequest.Password, user))
            {
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                var claims = new List<Claim> { new Claim(ClaimTypes.Name, user.Email), new Claim(ClaimTypes.NameIdentifier, user.UserGuid) };
                //claims.Add(new Claim(ClaimTypes.Role, _userRoles[0]));

                var Sectoken = new JwtSecurityToken(_config["Jwt:Issuer"],
                  _config["Jwt:Issuer"],
                  claims,
                  expires: DateTime.Now.AddMinutes(120),
                  signingCredentials: credentials);

                var token = new JwtSecurityTokenHandler().WriteToken(Sectoken);

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
                _userService.CreateUser(registerRequest);
                return Ok();
            }
            return BadRequest();
        }

    }
}
