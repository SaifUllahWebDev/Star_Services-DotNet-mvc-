using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StarSecurityServices.ApplicationDbContext;
using StarSecurityServices.Models;

public class CashServiceController : Controller
{
    private readonly ApplicationDb _context;

    public CashServiceController(ApplicationDb context)
    {
        _context = context;
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CashServiceBooking booking)
    {
        // ✅ Get employee email from session
        var employeeEmail = HttpContext.Session.GetString("UserEmail");
        if (string.IsNullOrEmpty(employeeEmail))
        {
            ModelState.AddModelError("", "Session expired. Please log in again.");
            return View(booking);
        }

        // ✅ Assign before validation
        booking.EmployeeEmail = employeeEmail;
        booking.RequestedDate = DateTime.Now;
        booking.Status = "Pending";

        if (!ModelState.IsValid)
        {
            return View(booking);
        }

        _context.CashServiceBookings.Add(booking);
        await _context.SaveChangesAsync();

        TempData["Success"] = "Cash Service booking submitted successfully!";
        return RedirectToAction("Create");
    }

    public async Task<IActionResult> Index()
    {
        var bookings = await _context.CashServiceBookings.ToListAsync();
        return View(bookings);
    }

  

}
