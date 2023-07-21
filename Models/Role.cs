using System.ComponentModel.DataAnnotations;

namespace EventHub.Models
{
    public class Role
    {
        [Key]
        public int RoleId { get; set; }

        [MaxLength(50)]
        public string RoleName { get; set; } = string.Empty;

        [MaxLength(300)]
        public string RoleDescription { get; set; } = string.Empty;

        public ICollection<User>? Users { get; set; }
    }
}
