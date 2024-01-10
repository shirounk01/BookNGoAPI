using System.ComponentModel.DataAnnotations.Schema;

namespace BookNGoAPI.Models
{
    public class UserRole
    {
        public int UserRoleId { get; set; }
        [ForeignKey("User")]
        [Column(Order = 1)]
        public string UserGuid { get; set; }
        [ForeignKey("Role")]
        [Column(Order = 1)]
        public int RoleId { get; set; }
        public User? User { get; set; }
        public Role? Role { get; set; }
    }
}
