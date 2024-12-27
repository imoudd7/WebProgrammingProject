using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProject.Models;



namespace WebProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentApiController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public AppointmentApiController(ApplicationDbContext context)
        {
            this.context = context;
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
         Ucret = a.Ucret,
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
            if (appointment == null)
            {
                return BadRequest(new { message = "Invalid appointment data." });
            }


            if (appointment.AppointmentTime == DateTime.MinValue)
            {
                return BadRequest(new { message = "Appointment time is required." });
            }
            if (appointment.UserId == null)
            {
                return BadRequest(new { message = "User ID is required." });
            }
            if (appointment.SalonId == null)
            {
                return BadRequest(new { message = "Salon ID is required." });
            }





            var conflict = await context.Appointments
                        .Include(a => a.Salon)
                            .ThenInclude(s => s.Service)
                            .ThenInclude(sv => sv.Personal)
                        .Where(a => a.SalonId == appointment.SalonId &&
                                    a.AppointmentTime == appointment.AppointmentTime)
                        .FirstOrDefaultAsync();



            if (conflict != null)
            {
                return Conflict(new { message = "Bu saatte sectin personal mesgul." });
            }


            var user = await context.Users.FindAsync(appointment.UserId);
            if (user == null)
            {
                return NotFound(new { message = "User not found." });
            }


            appointment.CreatedAt = DateTime.UtcNow;

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