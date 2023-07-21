using System.ComponentModel.DataAnnotations;

namespace EventHub.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [MaxLength(100)]
        [Required(ErrorMessage = "Email cannot be left empty")]
        public string Email { get; set; } = string.Empty;

        [MaxLength(200)]
        [Required(ErrorMessage = "Password cannot be left empty")]
        public string Password { get; set; } = string.Empty;
         
        public int RoleId { get; set; }
        public Role? Role { get; set; }

    }
}
