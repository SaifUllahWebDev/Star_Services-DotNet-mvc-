using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StarSecurityServices.ApplicationDbContext;
using StarSecurityServices.Models;
using System.Web.Helpers;

namespace StarSecurityServices.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDb _context;

        public AdminController(ApplicationDb context)
        {
            _context = context;
        }

        // Dashboard Summary
        public IActionResult Dashboard()
        {
            ViewBag.GuardBookings = _context.GuardBookings.Count();
            ViewBag.CashBookings = _context.CashServiceBookings.Count();
            ViewBag.RecruitmentApps = _context.RecruitmentApplications.Count();
            ViewBag.ElectronicRequests = _context.ElectronicServiceRequests.Count();

            return View();
        }

        // Guard Bookings List
        public IActionResult GuardBookings()
        {
            var bookings = _context.GuardBookings.ToList();
            return View(bookings);
        }

        // Cash Bookings List
        

        // Recruitment Applications
        public IActionResult RecruitmentApplications()
        {
            var apps = _context.RecruitmentApplications
                .Include(r => r.RecruitmentAnnouncement)
                .ToList();
            return View(apps);
        }

        // Recruitment Announcements
        

        // Electronic Security Requests
        public IActionResult ElectronicServiceRequests()
        {
            var requests = _context.ElectronicServiceRequests
                .Include(r => r.Product)
                .ToList();
            return View(requests);
        }


        // Inside your existing controller or a new admin-specific controller
        public async Task<IActionResult> Index()
        {
            var bookings = await _context.GuardBookings.ToListAsync();
            return View(bookings);
        }

        [HttpPost]
        public async Task<IActionResult> Approve(int id)
        {
            var booking = await _context.GuardBookings.FindAsync(id);
            if (booking != null)
            {
                booking.Status = "Approved";
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("GuardBookings");
        }

        [HttpPost]
        public async Task<IActionResult> Reject(int id)
        {
            var booking = await _context.GuardBookings.FindAsync(id);
            if (booking != null)
            {
                booking.Status = "Rejected";
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("GuardBookings");
        }
        //CashServiceController CONTROLLER

        public async Task<IActionResult> CashServices(string status)
        {
            ViewBag.CurrentFilter = status;

            var bookings = _context.CashServiceBookings.AsQueryable();

            if (!string.IsNullOrEmpty(status))
            {
                bookings = bookings.Where(b => b.Status == status);
            }

            return View(await bookings.ToListAsync());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ApproveCashService(int id)
        {
            var booking = await _context.CashServiceBookings.FindAsync(id);
            if (booking != null)
            {
                booking.Status = "Approved";
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("CashServices");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RejectCashService(int id)
        {
            var booking = await _context.CashServiceBookings.FindAsync(id);
            if (booking != null)
            {
                booking.Status = "Rejected";
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("CashServices");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateCashServiceStatus(UpdateCashServiceStatus model)
        {
            if (ModelState.IsValid)
            {
                var booking = await _context.CashServiceBookings.FindAsync(model.BookingId);
                if (booking != null)
                {
                    booking.Status = model.Status;
                    _context.CashServiceBookings.Update(booking);
                    await _context.SaveChangesAsync();
                }
            }

            // Do NOT pass the status as filter
            return RedirectToAction("CashServices");
        }

        //RECUIRTMENT CONTROLLER

        // GET: View all announcements
        public IActionResult Recruitments()
        {
            var announcements = _context.RecruitmentAnnouncements.ToList();
            return View(announcements);
        }

        // GET: Create
        public IActionResult CreateAnnouncement()
        {
            return View();
        }

        // POST: Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAnnouncement(RecruitmentAnnouncement model)
        {
            if (ModelState.IsValid)
            {
                _context.RecruitmentAnnouncements.Add(model);
                await _context.SaveChangesAsync();
                return RedirectToAction("Recruitments");
            }
            return View(model);
        }

        // GET: Edit
        public async Task<IActionResult> EditAnnouncement(int id)
        {
            var announcement = await _context.RecruitmentAnnouncements.FindAsync(id);
            if (announcement == null) return NotFound();
            return View(announcement);
        }

        // POST: Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAnnouncement(RecruitmentAnnouncement model)
        {
            if (ModelState.IsValid)
            {
                _context.RecruitmentAnnouncements.Update(model);
                await _context.SaveChangesAsync();
                return RedirectToAction("Recruitments");
            }
            return View(model);
        }

        // GET: Delete
        public async Task<IActionResult> DeleteAnnouncement(int id)
        {
            var announcement = await _context.RecruitmentAnnouncements.FindAsync(id);
            if (announcement != null)
            {
                _context.RecruitmentAnnouncements.Remove(announcement);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Recruitments");
        }

        //Employee Panel content

        public async Task<IActionResult> Employees()
        {
            var employees = await _context.Employees.ToListAsync();
            return View(employees);
        }


        public IActionResult CreateEmployee()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateEmployee(Employee employee)
        {
            if (ModelState.IsValid)
            {
                // You should hash password here before saving
                employee.PasswordHash = BCrypt.Net.BCrypt.HashPassword(employee.PasswordHash);

                _context.Employees.Add(employee);
                await _context.SaveChangesAsync();
                return RedirectToAction("Employees");
            }
            return View(employee);
        }

        public async Task<IActionResult> EditEmployee(int id)
{
    var emp = await _context.Employees.FindAsync(id);
    if (emp == null) return NotFound();
    return View(emp);
}

[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> EditEmployee(Employee employee)
{
    if (ModelState.IsValid)
    {
        try
        {
            _context.Employees.Update(employee);
            await _context.SaveChangesAsync();
            return RedirectToAction("Employees");
        }
        catch (DbUpdateException ex)
        {
            // Optionally log the error
            ModelState.AddModelError("", "Unable to save changes.");
        }
    }

    return View(employee); // This will re-render the form with validation errors
}



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var emp = await _context.Employees.FindAsync(id);
            if (emp != null)
            {
                _context.Employees.Remove(emp);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Employees");
        }


        public async Task<IActionResult> ViewApplications()
        {
            var applications = await _context.RecruitmentApplications
                .Include(r => r.RecruitmentAnnouncement)
                .ToListAsync();

            return View(applications);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateApplicationStatus(int id, string status)
        {
            var app = await _context.RecruitmentApplications.FindAsync(id);
            if (app != null)
            {
                app.Status = status;
                _context.RecruitmentApplications.Update(app);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("ViewApplications");
        }

    }
}
