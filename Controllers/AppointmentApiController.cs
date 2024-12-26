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
        public IActionResult Post([FromBody] Appointments appointment)
        {
            // Validate the incoming model
            if (appointment == null)
            {
                return BadRequest("Invalid appointment data.");
            }

            if (appointment.AppointmentTime == DateTime.MinValue)
            {
                return BadRequest("Appointment time is required.");
            }

            if (appointment.Ucret <= 0)
            {
                return BadRequest("Ucret must be greater than zero.");
            }

            if (appointment.SalonId == null || appointment.UserId == null)
            {
                return BadRequest("SalonId and UserId are required.");
            }

            // Check if related entities exist
            var userExists = context.Users.Any(u => u.Id == appointment.UserId);
            var salonExists = context.Salons.Any(s => s.SalonId == appointment.SalonId);

            if (!userExists)
            {
                return NotFound($"User with ID {appointment.UserId} does not exist.");
            }

            if (!salonExists)
            {
                return NotFound($"Salon with ID {appointment.SalonId} does not exist.");
            }

            // Set default values for fields not provided
            appointment.CreatedAt = DateTime.UtcNow;
            appointment.Onay = false;

            // Add and save the appointment
            try
            {
                context.Appointments.Add(appointment);
                context.SaveChanges();
                return Ok($"{appointment.AppointmentId} has been added.");
            }
            catch (Exception ex)
            {
                // Log the error for debugging
                Console.WriteLine($"Error saving appointment: {ex.Message}");
                return StatusCode(500, "An error occurred while saving the appointment.");
            }
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