using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StarSecurityServices.ApplicationDbContext;
using StarSecurityServices.Models;

namespace StarSecurityServices.Controllers
{
    public class GuardBooking : Controller
    {
        private readonly ApplicationDb _context;
        private readonly IWebHostEnvironment _env;

        public GuardBooking(ApplicationDb context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // GET: GuardBooking/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(GuardBookings booking, IFormFile SupportingDocument)
        {
            Console.WriteLine("POST method hit");

            // ✅ Get employee email from session
            var employeeEmail = HttpContext.Session.GetString("UserEmail");
            if (string.IsNullOrEmpty(employeeEmail))
            {
                ModelState.AddModelError("", "Session expired. Please log in again.");
                return View(booking);
            }

            if (ModelState.IsValid)
            {
                booking.EmployeeEmail = employeeEmail;
                booking.CreatedAt = DateTime.Now;

                if (SupportingDocument != null && SupportingDocument.Length > 0)
                {
                    var uploadsFolder = Path.Combine(_env.WebRootPath, "documents");
                    Directory.CreateDirectory(uploadsFolder);

                    var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(SupportingDocument.FileName);
                    var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using var stream = new FileStream(filePath, FileMode.Create);
                    await SupportingDocument.CopyToAsync(stream);

                    booking.SupportingDocumentPath = "/documents/" + uniqueFileName;
                }

                _context.GuardBookings.Add(booking);
                await _context.SaveChangesAsync();

                TempData["Success"] = "Booking submitted successfully!";
                return RedirectToAction("Create");
            }

            foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
            {
                Console.WriteLine("Validation Error: " + error.ErrorMessage);
            }

            return View(booking);
        }
       


    }
}
