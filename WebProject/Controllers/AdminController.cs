using Microsoft.AspNetCore.Authorization;

using Microsoft.AspNetCore.Mvc;
namespace WebProject.Controllers;


[Authorize(Roles = "Admin")]
public class AdminController : Controller
{
    // private readonly ApplicationDbContext _context;

    // public AdminController(ApplicationDbContext context)
    // {
    //     _context = context;
    // }

    // public IActionResult Index()
    // {
    //     var currentUser = _context.Users.FirstOrDefault(u => u.Email == User.Identity.Name);

    //     if (currentUser == null || currentUser.Role != "Admin")
    //     {
    //         return Unauthorized(); // Prevent access
    //     }

    //     // Admin functionality
    //     return View();
    // }
}