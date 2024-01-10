using BookNGoAPI.Models;
using BookNGoAPI.Repositories.Interfaces;

namespace BookNGoAPI.Repositories
{
    public class UserRoleRepository:RepositoryBase<UserRole>, IUserRoleRepository
    {
        public UserRoleRepository(BookNGoContext locationContext) : base(locationContext)
        {
        }
    }
}
