// Models/UpdateCashServiceStatus.cs
using System.ComponentModel.DataAnnotations;

namespace StarSecurityServices.Models
{
    public class UpdateCashServiceStatus
    {
        [Required]
        public int BookingId { get; set; }

        [Required]
        public string Status { get; set; } = "Pending";  // "Approved" or "Rejected"
    }
}
