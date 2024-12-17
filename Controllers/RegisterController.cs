using Microsoft.AspNetCore.Mvc;
using WebProject.Models;
using Microsoft.EntityFrameworkCore;

namespace WebProject.Controllers;

public class RegisterController : Controller
{
    private readonly ApplicationDbContext _context;
    public RegisterController(ApplicationDbContext context)
    {
        _context = context;
        if (!_context.Users.Any(u => u.Role == UserRole.Admin))
        {
            // Create a list of admin users
            var admins = new List<User>
        {
            new User
            {
                Email = "G221210569@sakarya.edu.tr",
                Password = "sau",
                Role = UserRole.Admin
            },
            new User
            {
                Email = "G221210588@sakarya.edu.tr",
                Password ="sau",
                Role = UserRole.Admin
            }
        };


            _context.Users.AddRange(admins);
            _context.SaveChanges();
        }


    }

    public IActionResult Login()
    {
        return View();
    }



    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logging(User model)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.ErrorMessage = "Invalid login attempt.";
            return View("Login");
        }



        // Fetch the user from the database
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == model.Email);

        if (user == null)
        {
            TempData["msj"] = "User does not exist.";
            return View("Login");
        }

        if (user.Password != model.Password)
        {
            TempData["msj"] = "Incorrect password or email";
            return View("Login");
        }

        // Debug: Log the user role


        if (user.Role == UserRole.Admin)
        {
            Console.WriteLine("Redirecting to Admin Dashboard...");
            return RedirectToAction("AdminDashboard", "Admin");
        }

        return RedirectToAction("Index", "Home");
    }




    public IActionResult Signup()
    {
        return View();
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Signup(User model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }


        if (await _context.Users.AnyAsync(u => u.Email == model.Email))
        {
            ModelState.AddModelError("Email", "This email is already registered.");
            return View(model);
        }


        var newUser = new User
        {
            Email = model.Email,
            Password = model.Password,
            Role = UserRole.Customer
        };

        // Save the user to the database
        _context.Users.Add(newUser);
        await _context.SaveChangesAsync();


        return RedirectToAction("Login");
    }



}
