using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StarSecurityServices.ApplicationDbContext;
using StarSecurityServices.Models;

namespace StarSecurityServices.Controllers
{
    public class HomeController : Controller
    {

        private readonly ApplicationDb _context;

        public HomeController(ApplicationDb context)
        {
            _context = context;
        }
        

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Career()
        {
            return View();
        }

        public IActionResult NorthRegion()
        {
            return View();
        }
        public IActionResult SouthRegion()
        {
            return View();
        }
        public IActionResult EastRegion()
        {
            return View();
        }
        public IActionResult WestRegion()
        {
            return View();
        }
        public IActionResult NorthVacanices()
        {
            return View();
        }
        public IActionResult EastVacanices()
        {
            return View();
        }
        public IActionResult WestVacanices()
        {
            return View();
        }
        public IActionResult SouthVacanices()
        {
            return View();
        }
        public async Task<IActionResult> AvailableVacancies()
        {
            var vacancies = await _context.Vacancies
                .Where(v => !v.IsFilled)
                .ToListAsync();

            return View("AvailableVacancies", vacancies); // Reuse the same view
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
