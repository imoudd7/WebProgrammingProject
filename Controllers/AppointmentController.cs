
using Microsoft.AspNetCore.Mvc;
namespace WebProject.Controllers;
using Microsoft.EntityFrameworkCore;
using WebProject.Models;

public class AppointmentController : Controller
{

    private readonly ApplicationDbContext context;
    public AppointmentController(ApplicationDbContext context)
    {
        this.context = context;
    }


    public async Task<IActionResult> GetAllAppointments()
    {
        var Appointments = await context.Appointments.ToListAsync();
        return View(Appointments);
    }

    public async Task<IActionResult> GetOneAppointment(int id)
    {
        var appointment = await context.Appointments.FirstOrDefaultAsync(a => a.Id == id);
                                
        if (appointment == null)
        {
            return NotFound(); 
        }

        return View(appointment); 
    }


    [HttpPost]   
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("AppointmentTime, IsConfirmed, Ucret, PersonalId, UserId, ServiceId")] Appointments appointment)
    {
        if (ModelState.IsValid)
        {
         appointment.CreatedAt = DateTime.UtcNow; 
         context.Add(appointment); 
         await context.SaveChangesAsync(); 
         return RedirectToAction(nameof(Index)); 
        }

         return View(appointment);
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
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
        return context.Appointments.Any(e => e.Id == id);
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Update(int id, [Bind("AppointmentTime, IsConfirmed, Ucret, PersonalId, UserId, ServiceId")] Appointments updatedAppointment)
    {
        if (id != updatedAppointment.Id)
        {
            return BadRequest();
        }

        if (ModelState.IsValid)
        {
            try
            {
                context.Update(updatedAppointment);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AppointmentExists(updatedAppointment.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }

        return View(updatedAppointment); 
    }

    






}
