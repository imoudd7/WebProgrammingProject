using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<User> userManager;

        public AppointmentController(ApplicationDbContext context, UserManager<User> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }


        public IActionResult Index()
        {
            var salons = context.Salons.ToList();
            var services = context.Services.ToList();
            var personals = context.Personals
                                   .Select(p => new { p.PersonalID, FullName = p.Ad + " " + p.Soyad })
                                   .ToList();

            ViewBag.Salons = salons.Any()
                ? new SelectList(salons, "SalonId", "Name")
                : new SelectList(new List<object>(), "SalonId", "Name");

            ViewBag.Services = services.Any()
                ? new SelectList(services, "ServiceId", "Name")
                : new SelectList(new List<object>(), "ServiceId", "Name");

            ViewBag.Personals = personals.Any()
                ? new SelectList(personals, "PersonalID", "FullName")
                : new SelectList(new List<object>(), "PersonalID", "FullName");

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
            var userId = userManager.GetUserId(User);

            // If user is not logged in, handle accordingly
            if (string.IsNullOrEmpty(userId))
            {


                return RedirectToAction("Login", "Account");
            }


            appointment.UserId = userId;

            if (appointment.AppointmentTime == DateTime.MinValue)
            {
                ModelState.AddModelError("AppointmentTime", "Appointment time is required.");
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
      .FirstOrDefaultAsync(s => s.SalonId == appointment.SalonId);


            var service = await context.Services
                .FirstOrDefaultAsync(s => s.ServiceId == appointment.ServiceId);
            var Person = await context.Personals
            .FirstOrDefaultAsync(s => s.PersonalID == appointment.PersonalID);



            if (salon == null || service == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid salon or service.");
                return View(appointment);
            }

            if (Person == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid personal.");
                return View(appointment);
            }


            DateTime newAppointmentStart = appointment.AppointmentTime;


            TimeSpan duration = service.Duration;
            DateTime newAppointmentEnd = newAppointmentStart.Add(duration);
            var potentialConflicts = await (
                from ap in context.Appointments
                where ap.PersonalID == appointment.PersonalID
                      && ap.AppointmentTime < newAppointmentEnd
                join sr in context.Services on ap.ServiceId equals sr.ServiceId
                select new
                {
                    AppointmentId = ap.AppointmentId,
                    AppointmentStart = ap.AppointmentTime,
                    ServiceDuration = sr.Duration
                }
            ).ToListAsync();
            var conflict = potentialConflicts.FirstOrDefault(pc =>
                newAppointmentStart < pc.AppointmentStart.Add(pc.ServiceDuration)
                && pc.AppointmentStart < newAppointmentEnd
            );



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
                Person.Earnings += service.Price;

                context.Appointments.Add(appointment);


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