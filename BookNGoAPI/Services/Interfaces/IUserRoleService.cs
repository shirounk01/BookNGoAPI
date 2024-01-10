using System.Security.Claims;

namespace BookNGoAPI.Services.Interfaces
{
    public interface IUserRoleService
    {
        void AddAdmin(string guid);
        void AddUser(string guid);
        string GetRoleName(string guid);
    }
}
