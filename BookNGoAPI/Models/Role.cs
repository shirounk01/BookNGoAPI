namespace BookNGoAPI.Models
{
    public class Role
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public ICollection<UserRole>? Roles { get; set; }
    }
}
