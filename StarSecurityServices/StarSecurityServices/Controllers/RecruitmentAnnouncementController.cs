using Microsoft.AspNetCore.Mvc;
using StarSecurityServices.Models;
using StarSecurityServices.ApplicationDbContext;

namespace StarSecurityServices.Controllers
{
    public class RecruitmentAnnouncementController : Controller
    {
        private readonly ApplicationDb _context;

        public RecruitmentAnnouncementController(ApplicationDb context)
        {
            _context = context;
        }

        // GET: Create a new training announcement
        public IActionResult Create()
        {
            return View();
        }

        // POST: Save announcement
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create(RecruitmentAnnouncement announcement)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        announcement.CreatedAt = DateTime.Now;
        //        _context.RecruitmentAnnouncements.Add(announcement);
        //        await _context.SaveChangesAsync();
        //        TempData["Success"] = "Training announcement posted!";
        //        return RedirectToAction("Create");
        //    }
        //    return View(announcement);
        //}

        // GET: List of announcements
        public IActionResult List()
        {
            var announcements = _context.RecruitmentAnnouncements.ToList();
            return View(announcements);
        }
    }
}
