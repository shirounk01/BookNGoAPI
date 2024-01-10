using BookNGoAPI.Models;
using BookNGoAPI.Repositories.Interfaces;

namespace BookNGoAPI.Repositories
{
    public class RoleRepository: RepositoryBase<Role>, IRoleRepository
    {
        public RoleRepository(BookNGoContext locationContext) : base(locationContext)
        {
        }
    }
}
