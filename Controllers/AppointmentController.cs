
using Microsoft.AspNetCore.Mvc;
namespace WebProject.Controllers;

public class AppointmentController : Controller
{

    private readonly ApplicationDbContext context;
    public AppointmentController(ApplicationDbContext context)
    {
        this.context = context;
    }


    public IActionResult GetAllAppointments()
    {
        var Appointments = context.Appointments.ToList();
        return View(Appointments);
    }
}