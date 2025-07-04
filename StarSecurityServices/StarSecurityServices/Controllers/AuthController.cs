using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StarSecurityServices.ApplicationDbContext;
using StarSecurityServices.ViewModels;
using System;
using System.Threading.Tasks;
using BCrypt.Net;

namespace StarSecurityServices.Controllers
{
    public class AuthController : Controller
    {
        private readonly ApplicationDb _context;

        public AuthController(ApplicationDb context)
        {
            _context = context;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var employee = await _context.Employees.FirstOrDefaultAsync(e =>
                    e.Email == model.Email &&
                    e.FullName == model.Name &&
                    e.EmployeeCode == model.EmployeeCode);

                if (employee != null)
                {
                    // 🔐 Check hashed password using BCrypt
                    bool isPasswordValid = BCrypt.Net.BCrypt.Verify(model.Password, employee.PasswordHash);

                    // 🔍 Debug log
                    Console.WriteLine("🔐 Input Password: " + model.Password);
                    Console.WriteLine("🗃️ Stored Hash: " + employee.PasswordHash);
                    Console.WriteLine("✅ Match Result: " + isPasswordValid);

                    if (isPasswordValid)
                    {
                        // ✅ Set session
                        HttpContext.Session.SetString("UserName", employee.FullName);
                        HttpContext.Session.SetString("UserEmail", employee.Email);
                        HttpContext.Session.SetString("UserRole", employee.Role);

                        return employee.Role == "Admin"
                            ? RedirectToAction("Dashboard", "Admin")
                            : RedirectToAction("Dashboard", "Employee");
                    }
                }

                TempData["Error"] = "Invalid login credentials.";
            }

            return View(model);
        }

        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Auth");
        }

    }
}
