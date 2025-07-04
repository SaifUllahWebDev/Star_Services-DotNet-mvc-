using System.ComponentModel.DataAnnotations;

namespace StarSecurityServices.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Employee Name")]
        [StringLength(100)]
        public string FullName { get; set; }

        [Required]
        [Display(Name = "Address")]
        [StringLength(200)]
        public string Address { get; set; }

        [Required]
        [Display(Name = "Contact Number")]
        [Phone]
        public string ContactNumber { get; set; }

        [Required]
        [Display(Name = "Educational Qualification")]
        [StringLength(100)]
        public string Qualification { get; set; }

        [Required]
        [Display(Name = "Employee Code")]
        [StringLength(50)]
        public string EmployeeCode { get; set; }

        [Required]
        [StringLength(100)]
        public string Department { get; set; }

        [Required]
        [StringLength(50)]
        public string Role { get; set; }  // "Admin" or "Employee"

        [Required]
        [StringLength(50)]
        public string Grade { get; set; }

        [Required]
        [StringLength(100)]
        public string Client { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; }

        [Required]
        public string PasswordHash { get; set; }
    }
}
