using Microsoft.AspNetCore.Authorization;

using Microsoft.AspNetCore.Mvc;
namespace WebProject.Controllers;


[Authorize(Roles = "Admin")]
public class AdminController : Controller
{
    private readonly ApplicationDbContext _context;

    public AdminController(ApplicationDbContext context)
    {
        _context = context;
    }
    public ActionResult AdminDashboard()
    {
        return View();
    }
}