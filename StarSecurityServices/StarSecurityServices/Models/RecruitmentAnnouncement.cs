using System;
using System.ComponentModel.DataAnnotations;

namespace StarSecurityServices.Models
{
    public class RecruitmentAnnouncement
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        [StringLength(150)]
        public string Title { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Training date is required.")]
        [DataType(DataType.Date)]
        public DateTime TrainingDate { get; set; }  // Acts as StartDate

        [Required(ErrorMessage = "End date is required.")]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }



        [Required(ErrorMessage = "Location is required.")]
        [StringLength(100)]
        public string Location { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
