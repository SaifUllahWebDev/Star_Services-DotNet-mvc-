using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StarSecurityServices.Models
{
    public class ElectronicServiceRequest
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ProductId { get; set; }

        public string? EmployeeEmail { get; set; }
        [ForeignKey("ProductId")]
        public ElectronicSecurityProduct? Product { get; set; }

        [Required(ErrorMessage = "Customer name is required.")]
        public string CustomerName { get; set; }

        [Required(ErrorMessage = "Contact number is required.")]
        [Phone(ErrorMessage = "Enter a valid phone number.")]
        public string ContactNumber { get; set; }

        [EmailAddress(ErrorMessage = "Enter a valid email.")]
        public string? Email { get; set; }  // Optional

        [Required(ErrorMessage = "Please select a request type.")]
        public string RequestType { get; set; }  // e.g. Installation, Maintenance

        [Required(ErrorMessage = "Address is required.")]
        public string Address { get; set; }

        public string? AdditionalNotes { get; set; }  // Optional

        public DateTime RequestDate { get; set; } = DateTime.Now;

        public string Status { get; set; } = "Pending";
    }
}
