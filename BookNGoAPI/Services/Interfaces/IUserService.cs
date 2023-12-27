using BookNGoAPI.Models;
using BookNGoAPI.Models.DTOs;

namespace BookNGoAPI.Services.Interfaces
{
    public interface IUserService
    {
        User FindByEmail(string email);
        bool CheckPassword(string password, User user);
        void CreateUser(UserInfo registerRequest);
        string GetGuid();
    }
}
