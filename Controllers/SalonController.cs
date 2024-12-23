using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProject.Models;

namespace WebProject.Controllers
{
    public class SalonController : Controller
    {
        private readonly ApplicationDbContext context;

        public SalonController(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<IActionResult> Index()
        {
            var salons = await context.Salons.ToListAsync();
            return View(salons);
        }

        public async Task<IActionResult> GetOneSalon(int id)
        {
            var salon = await context.Salons.FirstOrDefaultAsync(s => s.SalonId == id);

            if (salon == null)
            {
                return NotFound(); 
            }

            return View(salon); 
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Salon salon)
        {
            if (ModelState.IsValid)
            {
                context.Salons.Add(salon);
                await context.SaveChangesAsync(); 
                TempData["SuccessMessage"] = "Salon created successfully!";
                return RedirectToAction(nameof(Index)); 
            }
            return View(salon); 
        }

        public async Task<IActionResult> Edit(int id)
        {
            var salon = await context.Salons.FindAsync(id);
            if (salon == null)
            {
                return NotFound(); 
            }
            return View(salon); 
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Salon salon)
        {
            if (ModelState.IsValid)
            {
                context.Entry(salon).State = EntityState.Modified; 
                await context.SaveChangesAsync(); 
                TempData["SuccessMessage"] = "Salon updated successfully!";
                return RedirectToAction(nameof(Index)); 
            }
            return View(salon); 
        }

        public async Task<IActionResult> Delete(int id)
        {
            var salon = await context.Salons.FindAsync(id);
            if (salon == null)
            {
                return NotFound(); 
            }
            return View(salon); 
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var salon = await context.Salons.FindAsync(id);
            if (salon != null)
            {
                context.Salons.Remove(salon);
                await context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Salon deleted successfully!";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
