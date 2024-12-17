
using Microsoft.AspNetCore.Mvc;
namespace WebProject.Controllers;

public class AppointmentController : Controller
{
    public IActionResult Randevu()
    {
        return View();
    }
}