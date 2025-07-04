using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StarSecurityServices.ApplicationDbContext;
using StarSecurityServices.Models;

public class RecruitmentApplicationController : Controller
{
    private readonly ApplicationDb _context;

    public RecruitmentApplicationController(ApplicationDb context)
    {
        _context = context;
    }

    // GET
    public IActionResult Apply(int announcementId)
    {
        var announcement = _context.RecruitmentAnnouncements.Find(announcementId);
        if (announcement == null)
        {
            return NotFound();
        }

        var model = new RecruitmentApplication
        {
            AnnouncementId = announcementId
        };

        return View(model);
    }

    // POST
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Apply(RecruitmentApplication application)
    {
        if (ModelState.IsValid)
        {
            // Get the logged-in employee's email from session
            var employeeEmail = HttpContext.Session.GetString("UserEmail");

            if (string.IsNullOrEmpty(employeeEmail))
            {
                ModelState.AddModelError("", "Session expired. Please log in again.");
                return View(application);
            }

            application.EmployeeEmail = employeeEmail;
            application.SubmittedAt = DateTime.Now;
            application.Status = "Pending";

            _context.RecruitmentApplications.Add(application);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Application submitted successfully!";
            return RedirectToAction("Apply", new { announcementId = application.AnnouncementId });
        }

        return View(application); // if validation failed
    }

  

}
