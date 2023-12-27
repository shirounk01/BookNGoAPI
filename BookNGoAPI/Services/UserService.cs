using BookNGoAPI.Models;
using BookNGoAPI.Models.DTOs;
using BookNGoAPI.Repositories.Interfaces;
using BookNGoAPI.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BookNGoAPI.Services
{
    public class UserService : IUserService
    {
        private readonly IConfiguration _config;
        private readonly IRepositoryWrapper _repo;
        private readonly IHttpContextAccessor _context;

        public UserService(IConfiguration config,IRepositoryWrapper repo, IHttpContextAccessor context)
        {
            _config = config;
            _repo = repo;
            _context = context;
        }

        public bool CheckPassword(string password, User user)
        {
            return password.Equals(user.Password); // change to encription
        }

        public User CreateUser(UserInfo registerRequest)
        {
            User user = new User();
            user.Password = registerRequest.Password;
            user.Email = registerRequest.Email;

            _repo.UserRepository.Create(user);
            _repo.Save();

            return user;
        }

        public User FindByEmail(string email)
        {
            User user = _repo.UserRepository.FindByCondition(item => item.Email == email).FirstOrDefault();
            return user;
        }

        public string GetGuid()
        {
            string accessToken = _context.HttpContext.Request.Headers["Authorization"];


            // Remove "Bearer " prefix to get the token
            accessToken = accessToken.Substring("Bearer ".Length);
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(accessToken) as JwtSecurityToken;

            // Retrieve the "sub" claim (user identifier)
            var userGuid = jsonToken.Claims.FirstOrDefault(claim => claim.Type.Contains("nameidentifier"))?.Value;



            // Use the token as needed
            return userGuid;
        }

        public string GenerateToken(User user)
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
            return token;
        }
    }

}
