using BookNGoAPI.Models;
using BookNGoAPI.Models.DTOs;
using BookNGoAPI.Repositories.Interfaces;
using BookNGoAPI.Services.Interfaces;

namespace BookNGoAPI.Services
{
    public class UserService : IUserService
    {
        private readonly IRepositoryWrapper _repo;

        public UserService(IRepositoryWrapper repo)
        {
            _repo = repo;
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
            User user = _repo.UserRepository.FindByCondition(item=>item.Email == email).FirstOrDefault();
            return user;
        }

    }

}
