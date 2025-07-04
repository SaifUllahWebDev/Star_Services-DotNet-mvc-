using Microsoft.EntityFrameworkCore;
using StarSecurityServices.Models;

namespace StarSecurityServices.ApplicationDbContext
{
    public class ApplicationDb : DbContext
    {

        public ApplicationDb(DbContextOptions<ApplicationDb> options)
           : base(options)
        {
        }

        public DbSet<GuardBookings> GuardBookings { get; set; }

        public DbSet<CashServiceBooking> CashServiceBookings { get; set; }

        public DbSet<RecruitmentAnnouncement> RecruitmentAnnouncements { get; set; }
        public DbSet<RecruitmentApplication> RecruitmentApplications { get; set; }


        public DbSet<ElectronicSecurityProduct> ElectronicSecurityProducts { get; set; }
        public DbSet<ElectronicServiceRequest> ElectronicServiceRequests { get; set; }


        public DbSet<Employee> Employees { get; set; }

        public DbSet<Vacancy> Vacancies { get; set; }




    }
}
