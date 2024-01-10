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
        private readonly IUserRoleService _userRoleService;

        public UserService(IConfiguration config, IRepositoryWrapper repo, IHttpContextAccessor context, IUserRoleService userRoleService)
        {
            _config = config;
            _repo = repo;
            _context = context;
            _userRoleService = userRoleService;
        }

        public bool CheckPassword(string password, User user)
        {
            System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
            System.Text.Decoder utf8Decode = encoder.GetDecoder();
            byte[] todecode_byte = Convert.FromBase64String(user.Password);
            int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
            char[] decoded_char = new char[charCount];
            utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
            string result = new String(decoded_char);
            return result.Equals(password);
        }

        public User CreateUser(UserInfo registerRequest)
        {
            User user = new User();
            byte[] encData_byte = new byte[registerRequest.Password.Length];
            encData_byte = System.Text.Encoding.UTF8.GetBytes(registerRequest.Password);
            user.Password = Convert.ToBase64String(encData_byte);
            user.Email = registerRequest.Email;

            _repo.UserRepository.Create(user);

            if (user.Email.Contains("@bookngo.com"))
            {
                _userRoleService.AddAdmin(user.UserGuid);
            }
            else
            {
                _userRoleService.AddUser(user.UserGuid);
            }

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

            var claims = new List<Claim> { new Claim(ClaimTypes.Email, user.Email), new Claim(ClaimTypes.NameIdentifier, user.UserGuid) };

            string roleName = _userRoleService.GetRoleName(user.UserGuid);
            claims.Add(new Claim(ClaimTypes.Role, roleName));


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
