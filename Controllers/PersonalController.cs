using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProject.Models;

namespace WebProject.Controllers
{
    public class PersonalController : Controller
    {
        private readonly ApplicationDbContext context;

        public PersonalController(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<IActionResult> Index()
        {
            var personals = await context.Personals.ToListAsync();
            return View(personals);
        }

        public async Task<IActionResult> GetOnePersonal(int id)
        {
            var personal = await context.Personals.FirstOrDefaultAsync(a => a.PersonalID == id);

            if (personal == null)
            {
                return NotFound();
            }

            return View(personal);
        }

        public IActionResult Create()
        {
            return View(new Personal());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Personal personal)
        {
            if (string.IsNullOrWhiteSpace(personal.Ad) || string.IsNullOrWhiteSpace(personal.Soyad) || string.IsNullOrWhiteSpace(personal.Uzmanlik))
            {
                ModelState.AddModelError("", "Please fill in all the fields");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    context.Personals.Add(personal);
                    await context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Done";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"{ex.Message}");
                }
            }

            return View(personal);
        }
        [HttpGet("Personal/Edit/{id}")]

        public async Task<IActionResult> Edit(int id)
        {
            var personal = await context.Personals.FindAsync(id);
            if (personal == null)
            {
                return NotFound();
            }
            return View(personal);
        }

        [HttpPost("Personal/Edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditConfirm(int id, Personal person)
        {
            if (id != person.PersonalID) return BadRequest("Mismatched ID");

            if (ModelState.IsValid)
            {
                context.Personals.Update(person);
                await context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Updated!";
                return RedirectToAction(nameof(Index));
            }
            return View(person);
        }



        public async Task<IActionResult> Delete(int id)
        {
            var personal = await context.Personals.FindAsync(id);
            if (personal == null)
            {
                return NotFound();
            }
            return View(personal);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var personal = await context.Personals.FindAsync(id);
            if (personal != null)
            {
                context.Personals.Remove(personal);
                await context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Personal deleted successfully!";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
