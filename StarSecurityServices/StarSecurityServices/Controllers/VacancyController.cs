using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StarSecurityServices.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using StarSecurityServices.ApplicationDbContext;

namespace StarSecurityServices.Controllers
{
    public class VacancyController : Controller
    {
        private readonly ApplicationDb _context;
        private readonly IWebHostEnvironment _env;

        public VacancyController(ApplicationDb context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // GET: Vacancy
        public async Task<IActionResult> Index()
        {
            var vacancies = await _context.Vacancies.ToListAsync();
            return View(vacancies);
        }

        // GET: Vacancy/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Vacancy/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Vacancy vacancy, IFormFile ImageFile)
        {
            if (ModelState.IsValid)
            {
                if (ImageFile != null && ImageFile.Length > 0)
                {
                    var uploadsFolder = Path.Combine(_env.WebRootPath, "uploads/vacancies");
                    Directory.CreateDirectory(uploadsFolder);
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(ImageFile.FileName);
                    var filePath = Path.Combine(uploadsFolder, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await ImageFile.CopyToAsync(stream);
                    }

                    vacancy.ImagePath = "/uploads/vacancies/" + fileName;
                }

                _context.Add(vacancy);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(vacancy);
        }

        // GET: Vacancy/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var vacancy = await _context.Vacancies.FindAsync(id);
            if (vacancy == null) return NotFound();

            return View(vacancy);
        }

        // POST: Vacancy/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Vacancy vacancy, IFormFile? ImageFile)
        {
            if (id != vacancy.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    if (ImageFile != null && ImageFile.Length > 0)
                    {
                        var uploadsFolder = Path.Combine(_env.WebRootPath, "uploads/vacancies");
                        Directory.CreateDirectory(uploadsFolder);
                        var fileName = Guid.NewGuid().ToString() + Path.GetExtension(ImageFile.FileName);
                        var filePath = Path.Combine(uploadsFolder, fileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await ImageFile.CopyToAsync(stream);
                        }

                        vacancy.ImagePath = "/uploads/vacancies/" + fileName;
                    }

                    _context.Update(vacancy);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Vacancies.Any(e => e.Id == vacancy.Id))
                        return NotFound();

                    throw;
                }

                return RedirectToAction("Index");
            }

            return View(vacancy);
        }

        // GET: Vacancy/Delete/5
        // GET: Vacancy/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var vacancy = await _context.Vacancies.FirstOrDefaultAsync(m => m.Id == id);
            if (vacancy == null) return NotFound();

            return View(vacancy);
        }

        // POST: Vacancy/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vacancy = await _context.Vacancies.FindAsync(id);
            if (vacancy == null) return NotFound();

            _context.Vacancies.Remove(vacancy);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }


        // POST: Vacancy/MarkFilled/5
        [HttpPost]
        public async Task<IActionResult> MarkFilled(int id)
        {
            var vacancy = await _context.Vacancies.FindAsync(id);
            if (vacancy == null)
            {
                return NotFound();
            }

            vacancy.IsFilled = true;
            _context.Update(vacancy);
            await _context.SaveChangesAsync();

            TempData["Message"] = "Vacancy marked as filled successfully.";
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> AvailableVacancies()
        {
            var vacancies = await _context.Vacancies
                .Where(v => !v.IsFilled)
                .ToListAsync();

            return View("AllVacancies", vacancies); // Reuse the same view
        }



    }
}
