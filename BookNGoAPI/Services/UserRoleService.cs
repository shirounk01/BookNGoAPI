using BookNGoAPI.Models;
using BookNGoAPI.Repositories.Interfaces;
using BookNGoAPI.Services.Interfaces;

namespace BookNGoAPI.Services
{
    public class UserRoleService:IUserRoleService
    {
        private readonly IRepositoryWrapper _repo;
        private readonly IRoleRepository _roleRepository;


        public UserRoleService(IRepositoryWrapper repo, IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
            _repo = repo;
        }

        public void AddUser(string guid)
        {
            var newLink = new UserRole();
            newLink.RoleId = 2;
            newLink.UserGuid = guid;
            _repo.UserRoleRepository.Create(newLink);
            _repo.Save();
        }
        public void AddAdmin(string guid)
        {

            var newLink = new UserRole();
            newLink.RoleId = 1;
            newLink.UserGuid = guid;
            _repo.UserRoleRepository.Create(newLink);
            _repo.Save();
        }

        public string GetRoleName(string guid)
        {
            int roleId = _repo.UserRoleRepository.FindByCondition(item => item.UserGuid == guid).FirstOrDefault().RoleId;
            string roleName = _repo.RoleRepository.FindByCondition(item => item.RoleId == roleId).FirstOrDefault().RoleName;
            return roleName;
        }
    }
}
