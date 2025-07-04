using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StarSecurityServices.ApplicationDbContext;
using StarSecurityServices.Models;
using System.Linq;

namespace StarSecurityServices.Controllers
{
    public class ElectronicServiceRequestController : Controller
    {
        private readonly ApplicationDb _context;

        public ElectronicServiceRequestController(ApplicationDb context)
        {
            _context = context;
        }

        public IActionResult Create()
        {
            ViewBag.Products = new SelectList(_context.ElectronicSecurityProducts, "Id", "SystemType");
            return View();
        }

        // POST: Create Request
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ElectronicServiceRequest request)
        {
            if (ModelState.IsValid)
            {
                // ✅ Get logged-in user email
                var userEmail = HttpContext.Session.GetString("UserEmail");
                if (string.IsNullOrEmpty(userEmail))
                {
                    TempData["Error"] = "You must be logged in to submit a request.";
                    return RedirectToAction("Login", "Auth");
                }

                request.EmployeeEmail = userEmail;
                request.RequestDate = DateTime.Now;
                request.Status = "Pending";

                _context.ElectronicServiceRequests.Add(request);
                await _context.SaveChangesAsync();

                TempData["Success"] = "Your request has been submitted.";
                return RedirectToAction("Create");
            }

            ViewBag.Products = new SelectList(_context.ElectronicSecurityProducts, "Id", "SystemType", request.ProductId);
            return View(request);
        }

        // 🔹 Admin View: All Requests with optional filters
        public async Task<IActionResult> ViewRequests(string requestType, int? productId)
        {
            var query = _context.ElectronicServiceRequests
                                .Include(r => r.Product)
                                .AsQueryable();

            if (!string.IsNullOrEmpty(requestType))
                query = query.Where(r => r.RequestType == requestType);

            if (productId.HasValue)
                query = query.Where(r => r.ProductId == productId.Value);

            ViewBag.Products = new SelectList(await _context.ElectronicSecurityProducts.ToListAsync(), "Id", "SystemType");
            ViewBag.SelectedType = requestType;
            ViewBag.SelectedProductId = productId;

            var requests = await query.ToListAsync();
            return View(requests);
        }

        // 🔹 Admin Action: Approve or Reject Request
        [HttpPost]
        public async Task<IActionResult> UpdateStatus(int id, string status)
        {
            var request = await _context.ElectronicServiceRequests.FindAsync(id);
            if (request == null)
                return NotFound();

            if (status == "Approved" || status == "Rejected")
            {
                request.Status = status;
                await _context.SaveChangesAsync();
                TempData["Success"] = $"Request {status.ToLower()} successfully.";
            }

            return RedirectToAction("ViewRequests");
        }

        // 🔹 Employee View: My Requests
        public async Task<IActionResult> MyRequests()
        {
            var userEmail = HttpContext.Session.GetString("UserEmail");

            if (string.IsNullOrEmpty(userEmail))
            {
                TempData["Error"] = "You must be logged in to view your requests.";
                return RedirectToAction("Login", "Auth");
            }

            var requests = await _context.ElectronicServiceRequests
                .Include(r => r.Product)
                .Where(r => r.Email == userEmail)
                .ToListAsync();

            return View(requests);
        }

        // 🔹 Employee Action: Cancel Their Own Pending Request
       

    }
}
