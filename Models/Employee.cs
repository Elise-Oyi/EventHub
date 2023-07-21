using System.ComponentModel.DataAnnotations;

namespace EventHub.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }

        [MaxLength(100)]
        [Required(ErrorMessage = "Employee name cannot be left empty")]
        public string EmployeeName { get; set; } = string.Empty;

        [MaxLength(300)]
        public string Address { get; set; } = string.Empty;

        [MaxLength(50)]
        [Required(ErrorMessage = "Employee city cannot be empty")]
        public string City { get; set; } = string.Empty;

        [MaxLength(10)]
        public string Zipcode { get; set; } = string.Empty;

        [MaxLength(50)]
        public string Country { get; set; } = string.Empty;

        [MaxLength(200)]
        public string Skillsets { get; set; } = string.Empty;

        [MaxLength(15)]
        [Required(ErrorMessage = "Employee phone number cannot be empty")]
        public string Phone { get; set; } = string.Empty;

        [MaxLength(100)]
        [Required(ErrorMessage = "Employee email cannot be empty")]
        public string Email { get; set; } = string.Empty;

        [MaxLength(200)]
        public string Avatar { get; set; } = string.Empty;
    }
}
