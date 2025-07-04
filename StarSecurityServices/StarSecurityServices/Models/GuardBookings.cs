using System.ComponentModel.DataAnnotations;

namespace StarSecurityServices.Models
{
    public class GuardBookings
    {
        [Key]
        public int BookingId { get; set; }

        public string? EmployeeEmail { get; set; }
        // Customer Information
        [Required(ErrorMessage = "Full name is required.")]
        [StringLength(100, ErrorMessage = "Full name cannot exceed 100 characters.")]
        public string FullName { get; set; }

        [StringLength(100)]
        public string CompanyName { get; set; } // Optional

        [Required(ErrorMessage = "Email address is required.")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone number is required.")]
        [Phone(ErrorMessage = "Please enter a valid phone number.")]
        public string PhoneNumber { get; set; }

        [Phone(ErrorMessage = "Please enter a valid alternate phone number.")]
        public string AlternatePhone { get; set; } // Optional

        // Service Details
        [Required(ErrorMessage = "Please select a guard type.")]
        public string GuardType { get; set; }

        [Required(ErrorMessage = "Number of guards is required.")]
        [Range(1, 50, ErrorMessage = "You can request between 1 and 50 guards.")]
        public int NumberOfGuards { get; set; }

        [Required(ErrorMessage = "Please specify the service type.")]
        public string ServiceType { get; set; }

        public string PreferredSkills { get; set; } // Optional

        // Location Details
        [Required(ErrorMessage = "Service address is required.")]
        public string Address { get; set; }

        [Required(ErrorMessage = "City is required.")]
        public string City { get; set; }

        [Required(ErrorMessage = "Region is required.")]
        public string Region { get; set; }

        public string Landmark { get; set; } // Optional

        // Booking Duration
        [Required(ErrorMessage = "Start date is required.")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "End date is required.")]
        public DateTime EndDate { get; set; }

        [Required(ErrorMessage = "Shift timing is required.")]
        public string ShiftTiming { get; set; }

        // Security Requirements / Notes
        public string SecurityDescription { get; set; } // Optional

        public bool RequiresArmedGuard { get; set; }

        public bool RequiresUniform { get; set; }

        public string PreviousSecurityExperience { get; set; } // Optional

        // File Upload
        public string? SupportingDocumentPath { get; set; } // Optional

        // Booking Status
        public string Status { get; set; } = "Pending";

        // Timestamps
        public DateTime CreatedAt { get; set; } = DateTime.Now;

    }
}
