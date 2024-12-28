using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebProject.Models;

namespace WebProject.Controllers
{
    [Authorize]
    public class AppointmentController : Controller
    {
        private readonly ApplicationDbContext context;

        public AppointmentController(ApplicationDbContext context)
        {
            this.context = context;
        }

      

        public async Task<IActionResult> GetAllAppointments()
        {
            var appointments = await context.Appointments
                .Include(a => a.User)
                .Include(a => a.Salon)
                .ToListAsync();

            return View(appointments);
        }



        public IActionResult Index()
        {
            var salons = context.Salons.ToList();
            ViewBag.Salons = salons.Any() ? new SelectList(salons, "SalonId", "Name") : new SelectList(new List<Salon>(), "SalonId", "Name");

            var services = context.Services.ToList();
            ViewBag.Services = services.Any() ? new SelectList(services, "ServiceId", "Name") : new SelectList(new List<Service>(), "ServiceId", "Name");

            var personals = context.Personals
                                .Select(p => new { p.PersonalID, FullName = p.Ad + " " + p.Soyad })
                                .ToList();
            ViewBag.Personals = personals.Any() ? new SelectList(personals, "PersonalID", "FullName") : new SelectList(new List<object>(), "PersonalID", "FullName");

            return View();
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(Appointments appointment, string action)
        {

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
                return View(appointment);
            }


            var salon = await context.Salons
                .Include(s => s.Service)
                    .ThenInclude(sv => sv.Personal)
                .FirstOrDefaultAsync(s => s.SalonId == appointment.SalonId);

            if (salon == null || salon.Service == null || salon.Service.Personal == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid salon, service, or personal information. Please check your selection.");
                return View(appointment);
            }


            var conflict = await context.Appointments
                .Include(a => a.Salon)
                    .ThenInclude(s => s.Service)
                    .ThenInclude(sv => sv.Personal)
                .Where(a => a.SalonId == appointment.SalonId &&
                            a.Salon.Service.ServiceId == salon.Service.ServiceId &&
                            a.Salon.Service.Personal.PersonalID == salon.Service.Personal.PersonalID &&
                            a.AppointmentTime == appointment.AppointmentTime)
                .FirstOrDefaultAsync();

            if (conflict != null)
            {
                ModelState.AddModelError(string.Empty, "An appointment already exists at this time with the selected salon, service, and personal.");
                return View(appointment);
            }


            var user = await context.Users.FindAsync(appointment.UserId);
            if (user == null)
            {
                ModelState.AddModelError("", "User not found!");
                return View(appointment);
            }



            if (action == "Onay")
            {
                context.Add(appointment);
                await context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Appointment created successfully!";
                return RedirectToAction(nameof(Index));
            }

            TempData["InfoMessage"] = "Appointment creation was canceled.";
            return View(appointment);
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