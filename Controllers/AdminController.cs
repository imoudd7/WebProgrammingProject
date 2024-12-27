using Microsoft.AspNetCore.Authorization;

using Microsoft.AspNetCore.Mvc;
namespace WebProject.Controllers;



public class AdminController : Controller
{


    [Authorize(Roles = "Admin")]
    public ActionResult AdminDashboard()
    {
        return View();
    }
}