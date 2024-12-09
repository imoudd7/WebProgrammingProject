
using Microsoft.AspNetCore.Mvc;
namespace WebProject.Controllers;

public class AppointmentController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}