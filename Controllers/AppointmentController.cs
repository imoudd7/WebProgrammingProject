using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProject.Models;

namespace WebProject.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly ApplicationDbContext context;

        public AppointmentController(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<IActionResult> Index()
        {
            var appointments = await context.Appointments.ToListAsync();
            return View(appointments);
        }

        public async Task<IActionResult> GetAllAppointments()
        {
            var appointments = await context.Appointments
                .Include(a => a.User)
                .Include(a => a.Salon)
                .ToListAsync();

            return View(appointments);
        }


        public async Task<IActionResult> GetOneAppointment(int id)
        {
            var appointment = await context.Appointments.FirstOrDefaultAsync(a => a.AppointmentId == id);

            if (appointment == null)
            {
                return NotFound();
            }

            return View(appointment);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Appointments appointment)
        {
            // Validate required fields
            if (appointment.AppointmentTime == DateTime.MinValue)
            {
                ModelState.AddModelError("AppointmentTime", "Appointment time is required.");
            }
            if (appointment.UserId == null)
            {
                ModelState.AddModelError("UserId", "User ID is required.");
            }
            if (appointment.SalonId == null)
            {
                ModelState.AddModelError("SalonId", "Salon ID is required.");
            }

            if (!ModelState.IsValid)
            {
                return View(appointment); // Return the view with errors
            }

            // Check if related entities exist
            var user = await context.Users.FindAsync(appointment.UserId);
            var salon = await context.Salons.FindAsync(appointment.SalonId);

            if (user == null || salon == null)
            {
                ModelState.AddModelError("", "User or salon is not found!");
                return View(appointment);
            }

            // Set additional properties
            appointment.CreatedAt = DateTime.UtcNow;
            appointment.Onay = false;

            // Save to database
            context.Add(appointment);
            await context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Appointment created successfully!";
            return RedirectToAction(nameof(Index));
        }



        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointment = await context.Appointments.FindAsync(id);
            if (appointment == null)
            {
                return NotFound();
            }
            return View(appointment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AppointmentId, AppointmentTime, Onay, Ucret, UserId, SalonId, CreatedAt")] Appointments appointment)
        {
            if (id != appointment.AppointmentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

                    var user = await context.Users.FindAsync(appointment.UserId);
                    var salon = await context.Salons.FindAsync(appointment.SalonId);

                    if (user == null || salon == null)
                    {
                        ModelState.AddModelError("", "user or salon is not found");
                        return View(appointment);
                    }


                    context.Update(appointment);
                    await context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AppointmentExists(appointment.AppointmentId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(appointment);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointment = await context.Appointments.FirstOrDefaultAsync(m => m.AppointmentId == id);
            if (appointment == null)
            {
                return NotFound();
            }

            return View(appointment);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var appointment = await context.Appointments.FindAsync(id);
            if (appointment == null)
            {
                return NotFound();
            }
            context.Appointments.Remove(appointment);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AppointmentExists(int id)
        {
            return context.Appointments.Any(e => e.AppointmentId == id);
        }
    }
}