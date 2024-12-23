using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace WebProject.Controllers;


[Authorize(Roles = "Admin")]
public class AdminController : Controller
{
    public ActionResult AdminDashboard()
    {
        return View();
    }
}