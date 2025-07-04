using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StarSecurityServices.ApplicationDbContext;
using StarSecurityServices.ViewModels;

namespace StarSecurityServices.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly ApplicationDb _context;

        public EmployeeController(ApplicationDb context)
        {
            _context = context;
        }


        public async Task<IActionResult> ViewBookings()
        {
            var employeeEmail = HttpContext.Session.GetString("UserEmail");

            if (string.IsNullOrEmpty(employeeEmail))
            {
                TempData["Error"] = "Please login first.";
                return RedirectToAction("Login", "Auth");
            }


            if (string.IsNullOrEmpty(employeeEmail))
                return RedirectToAction("Login", "Auth"); // fallback to login if not authenticated

            var viewModel = new AllBookingsViewModel
            {
                GuardBookings = await _context.GuardBookings
                    .Where(g => g.EmployeeEmail == employeeEmail)
                    .OrderByDescending(g => g.CreatedAt)
                    .ToListAsync(),

                CashServiceBookings = await _context.CashServiceBookings
                    .Where(c => c.EmployeeEmail == employeeEmail)
                    .OrderByDescending(c => c.RequestedDate)
                    .ToListAsync(),

                ElectronicServiceRequests = await _context.ElectronicServiceRequests
                    .Include(e => e.Product)
                    .Where(e => e.EmployeeEmail == employeeEmail)
                    .OrderByDescending(e => e.RequestDate)
                    .ToListAsync(),

                RecruitmentApplications = await _context.RecruitmentApplications
                    .Include(r => r.RecruitmentAnnouncement)
                    .Where(r => r.EmployeeEmail == employeeEmail)
                    .OrderByDescending(r => r.SubmittedAt)
                    .ToListAsync()
            };

            return View(viewModel);
        }


        public async Task<IActionResult> Dashboard()
        {
            var userEmail = HttpContext.Session.GetString("UserEmail");
            if (string.IsNullOrEmpty(userEmail))
                return RedirectToAction("Login", "Auth");

            var employee = await _context.Employees.FirstOrDefaultAsync(e => e.Email == userEmail);
            if (employee == null)
                return RedirectToAction("Login", "Auth");

            var bookings = await _context.GuardBookings
                .Where(b => b.BookingId == employee.Id)
                .OrderByDescending(b => b.StartDate) // Adjust this as per your schema
                .Take(5)
                .ToListAsync();

            ViewBag.Name = employee.FullName;
            ViewBag.Email = employee.Email;
            ViewBag.Role = employee.Role;

            return View(bookings); // Send bookings to the view
        }



        [HttpPost]
        public async Task<IActionResult> CancelBooking(string type, int id)
        {
            switch (type)
            {
                case "Guard":
                    var guard = await _context.GuardBookings.FindAsync(id);
                    if (guard != null) guard.Status = "Cancelled";
                    break;
                case "Cash":
                    var cash = await _context.CashServiceBookings.FindAsync(id);
                    if (cash != null) cash.Status = "Cancelled";
                    break;
                case "Electronic":
                    var elec = await _context.ElectronicServiceRequests.FindAsync(id);
                    if (elec != null) elec.Status = "Cancelled";
                    break;
                case "Recruitment":
                    var rec = await _context.RecruitmentApplications.FindAsync(id);
                    if (rec != null) rec.Status = "Cancelled";
                    break;
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("ViewBookings");
        }

    }

}
