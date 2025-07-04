using System;
using System.ComponentModel.DataAnnotations;

namespace StarSecurityServices.Models
{
    public class ElectronicSecurityProduct
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "System type is required.")]
        [StringLength(100)]
        public string SystemType { get; set; }  // e.g. CCTV, Fire Alarm

        [StringLength(500)]
        public string? Description { get; set; }  // Optional

        public string? ImagePath { get; set; }  // Optional file path for system image

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
