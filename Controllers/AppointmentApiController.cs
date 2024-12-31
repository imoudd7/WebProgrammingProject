using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProject.Models;



namespace WebProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentApiController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<User> userManager;
        public AppointmentApiController(ApplicationDbContext context, UserManager<User> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }



        [HttpGet]

        public List<Appointments> Get()
        {
            var Appointments = context.Appointments
     .Select(a => new Appointments
     {
         AppointmentId = a.AppointmentId,
         AppointmentTime = a.AppointmentTime,
         CreatedAt = a.CreatedAt,
         Onay = a.Onay,
         ServiceId = a.ServiceId,
         PersonalID = a.PersonalID,
         SalonId = a.SalonId,
         UserId = a.UserId
     }).ToList();


            return Appointments;
        }


        [HttpGet("{id}")]

        public ActionResult<Appointments> Get(int id)
        {
            var Appointment = context.Appointments.FirstOrDefault(a => a.AppointmentId == id);
            if (Appointment is null)
            {
                return NoContent();
            }
            return Appointment;
        }


        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Appointments appointment)
        {
            // var userId = userManager.GetUserId(User);

            // // If user is not logged in, handle accordingly
            // if (string.IsNullOrEmpty(userId))
            // {


            //     return RedirectToAction("Login", "Account");
            // }

            // appointment.UserId = userId;

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
                return BadRequest(appointment);
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
                return BadRequest("salon or service is invalid");
            }

            if (Person == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid personal.");
                return BadRequest("Personal error");
            }


            DateTime newAppointmentStart = appointment.AppointmentTime;


            TimeSpan duration = service.Duration;
            DateTime newAppointmentEnd = newAppointmentStart.Add(duration);
            // 2) Broad filter: same Personal, and the existing appointment starts before your end
            //    (You can also do an additional filter: a.AppointmentTime >= someMinimum if needed)
            var potentialConflicts = await (
                from ap in context.Appointments
                where ap.PersonalID == appointment.PersonalID
                      // optional partial filter: a.AppointmentTime < newAppointmentEnd
                      // (this alone is translatable, because it's just a DateTime < DateTime)
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
                // Overlap condition: [newStart, newEnd) vs [pc.AppointmentStart, pcEnd)
                newAppointmentStart < pc.AppointmentStart.Add(pc.ServiceDuration)
                && pc.AppointmentStart < newAppointmentEnd
            );



            if (conflict != null)
            {
                ModelState.AddModelError(string.Empty, "An appointment already exists at this time with the selected salon, service, and personal.");
                return BadRequest("An appointment already exists at this time with the selected salon, service, and personal");
            }



            // var user = await context.Users.FindAsync(appointment.UserId);
            // if (user == null)
            // {
            //     return NotFound(new { message = "User not found." });
            // }


            appointment.CreatedAt = DateTime.UtcNow;
            appointment.Onay = true;

            context.Add(appointment);
            await context.SaveChangesAsync();

            return Ok(new { message = "Appointment created successfully!", appointmentId = appointment.AppointmentId });
        }




        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Appointments appointment)
        {
            var existingAppointment = context.Appointments.FirstOrDefault(a => a.AppointmentId == id);
            if (existingAppointment is null)
            {
                return NotFound();
            }

            existingAppointment.CreatedAt = appointment.CreatedAt;
            context.Appointments.Update(existingAppointment);
            context.SaveChanges();
            return Ok(appointment.AppointmentId + " has been updated.");
        }




        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var appointment = context.Appointments.FirstOrDefault(a => a.AppointmentId == id);
            if (appointment is null)
            {
                return NotFound();
            }

            context.Appointments.Remove(appointment);
            context.SaveChanges();
            return Ok("Appointment has been deleted.");
        }





    }
}