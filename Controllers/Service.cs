using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProject.Models;

namespace WebProject.Controllers
{
    public class ServiceController : Controller
    {
        private readonly ApplicationDbContext context;

        public ServiceController(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<IActionResult> Index()
        {
            var Services = await context.Services.ToListAsync();
            return View(Services);
        }

        public async Task<IActionResult> GetOneService(int id)
        {
            var Service = await context.Services.FirstOrDefaultAsync(a => a.ServiceId == id);

            if (Service == null)
            {
                return NotFound(); 
            }

            return View(Service); 
        }




         public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Service service)
        {
            if (ModelState.IsValid)
            {
                context.Services.Add(service);
                await context.SaveChangesAsync(); 
                TempData["SuccessMessage"] = "Service created successfully!";
                return RedirectToAction(nameof(Index)); 
            }
            return View(service); 
        }


        public async Task<IActionResult> Edit(int id)
        {
            var service = await context.Services.FindAsync(id);
            if (service == null)
            {
                return NotFound(); 
            }
            return View(service); 
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Service service)
        {
            if (ModelState.IsValid)
            {
                context.Entry(service).State = EntityState.Modified; 
                await context.SaveChangesAsync(); 
                TempData["SuccessMessage"] = "Service updated successfully!";
                return RedirectToAction(nameof(Index)); 
            }
            return View(service); 
        }






        public async Task<IActionResult> Delete(int id)
        {
            var service = await context.Services.FindAsync(id);
            if (service == null)
            {
                return NotFound(); 
            }
            return View(service); 
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var service = await context.Services.FindAsync(id);
            if (service != null)
            {
                context.Services.Remove(service);
                await context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Service deleted successfully!";
            }
            return RedirectToAction(nameof(Index));
        }


    }
}
