using System.ComponentModel;

namespace BookNGoAPI.Models.DTOs
{
    public class UserInfo
    {
        [DefaultValue("user@email.com")]
        public string? Email { get; set; }
        [DefaultValue("password")]
        public string? Password { get; set; }
    }
}
