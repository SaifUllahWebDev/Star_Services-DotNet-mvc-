using StarSecurityServices.Models;

namespace StarSecurityServices.ViewModels
{
    public class AllBookingsViewModel
    {
        public List<GuardBookings> GuardBookings { get; set; }
        public List<CashServiceBooking> CashServiceBookings { get; set; }
        public List<ElectronicServiceRequest> ElectronicServiceRequests { get; set; }
        public List<RecruitmentApplication> RecruitmentApplications { get; set; }
    }
}
