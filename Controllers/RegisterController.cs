using Microsoft.AspNetCore.Mvc;
using WebProject.Models;
using Microsoft.EntityFrameworkCore;

namespace WebProject.Controllers;

public class RegisterController : Controller
{
    private readonly ApplicationDbContext _context;
    private List<(string Email, string Password, UserRole Role)> Users { get; set; } = new();

    public static void Adminekle(ApplicationDbContext context)
    {
        if (!context.Users.Any(u => u.Role == UserRole.Admin))
        {
            var admin1 = new User
            {
                Email = "G221210569@sakarya.edu.tr",
                Password = HashPassword("sau"),
                Role = UserRole.Admin
            };

            var admin2 = new User
            {
                Email = "G221210588@sakarya.edu.tr",
                Password = HashPassword("sau"),
                Role = UserRole.Admin
            };

            context.Users.AddRange(admin1, admin2);
            context.SaveChanges();
        }
    }



    public RegisterController(ApplicationDbContext context)
    {
        _context = context;

        Adminekle(_context);

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

        var hashedPassword = HashPassword(model.Password);

        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == model.Email);

        if (user == null)
        {
            ViewBag.ErrorMessage = "User does not exist.";
            return View("Login");
        }

        if (user.Password != hashedPassword)
        {
            ViewBag.ErrorMessage = "Incorrect password.";
            return View("Login");
        }

        if (user.Role == UserRole.Admin)
        {
            return RedirectToAction("Dashboard", "Admin");
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
            return View(model); // Return view with validation errors
        }

        // Check if the email already exists in the database
        if (await _context.Users.AnyAsync(u => u.Email == model.Email))
        {
            ModelState.AddModelError("Email", "This email is already registered.");
            return View(model);
        }


        var newUser = new User
        {
            Email = model.Email,
            Password = HashPassword(model.Password),
            Role = UserRole.Customer // Assign default role
        };

        // Save the user to the database
        _context.Users.Add(newUser);
        await _context.SaveChangesAsync();

        // Redirect to login page after successful signup
        return RedirectToAction("Login");
    }


    private static string HashPassword(string? password)
    {
        using (var sha256 = System.Security.Cryptography.SHA256.Create())
        {
            var bytes = System.Text.Encoding.UTF8.GetBytes(password ?? string.Empty);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }
    }
}
