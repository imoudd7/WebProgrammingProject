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
            CreatedAt = a.CreatedAt,
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
        context.Appointments.Add(appointment);
        context.SaveChanges();
        return Ok(appointment.AppointmentId + " has been added.");
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