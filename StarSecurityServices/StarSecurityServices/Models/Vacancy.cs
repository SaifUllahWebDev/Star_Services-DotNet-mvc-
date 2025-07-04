using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;

namespace StarSecurityServices.Models
{
    public class Vacancy
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Job title is required")]
        [StringLength(100)]
        public string JobTitle { get; set; }

        [Required(ErrorMessage = "Job description is required")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Salary is required")]
        [Range(0, double.MaxValue, ErrorMessage = "Enter a valid salary")]
        [Precision(18, 2)] // EF Core 6+ built-in attribute
        public decimal Salary { get; set; }


        [Required(ErrorMessage = "Timing is required")]
        public string Timing { get; set; }

        [Required(ErrorMessage = "Region is required")]
        public string Region { get; set; } // North, South, East, West

        public string? ImagePath { get; set; }

        public bool IsFilled { get; set; } = false;

        public DateTime PostedOn { get; set; } = DateTime.Now;
    }
}
