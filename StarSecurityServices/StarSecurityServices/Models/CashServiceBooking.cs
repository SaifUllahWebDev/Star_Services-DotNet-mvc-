using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace StarSecurityServices.Models
{
    public class CashServiceBooking
    {
        public int Id { get; set; }
        public string? EmployeeEmail { get; set; }

        [Required(ErrorMessage = "Service type is required.")]
        public string ServiceType { get; set; } // "Cash Transfer" or "ATM Replenishment"

        [Required(ErrorMessage = "Amount is required.")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "Source location is required.")]
        public string SourceLocation { get; set; }

        [Required(ErrorMessage = "Destination location is required.")]
        public string DestinationLocation { get; set; }

        public string Status { get; set; } = "Pending";

        [Required(ErrorMessage = "Requested date is required.")]
        [DataType(DataType.Date)]
        public DateTime RequestedDate { get; set; }

        public string Instructions { get; set; } // Optional
    }
}
