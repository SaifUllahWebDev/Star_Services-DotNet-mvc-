using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StarSecurityServices.Models
{
    public class RecruitmentApplication
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("RecruitmentAnnouncement")]
        public int AnnouncementId { get; set; }

        public string? EmployeeEmail { get; set; }
        public RecruitmentAnnouncement? RecruitmentAnnouncement { get; set; }  // <-- this line

        [Required]
        public string PersonName { get; set; }

        [Required]
        public string ContactNumber { get; set; }

        [Required]
        public string Skills { get; set; }

        public string Status { get; set; } = "Pending";  // Default value

        public DateTime SubmittedAt { get; set; } = DateTime.Now;
    }
}
