using BookNGoAPI.Models;
using BookNGoAPI.Models.DTOs;
using BookNGoAPI.Repositories.Interfaces;
using BookNGoAPI.Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;

namespace BookNGoAPI.Services
{
    public class UserService : IUserService
    {
        private readonly IRepositoryWrapper _repo;
        private readonly IHttpContextAccessor _context;

        public UserService(IRepositoryWrapper repo, IHttpContextAccessor context)
        {
            _repo = repo;
            _context = context;
        }

        public bool CheckPassword(string password, User user)
        {
            return password.Equals(user.Password); // change to encription
        }

        public void CreateUser(UserInfo registerRequest)
        {
            User user = new User();
            user.Password = registerRequest.Password;
            user.Email = registerRequest.Email;

            _repo.UserRepository.Create(user);
            _repo.Save();
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

    }

}
