using Microsoft.AspNetCore.Mvc;
using WebProject.Models; // تأكد من استخدام المسار الصحيح للنموذج

namespace WebProject.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult AdminDashboard()
        {
       
            var employees = _context.Personals.ToList();

            var AppointmentTotal = _context.Users.Count();  
            ViewData["AppointmentTotal"] = AppointmentTotal;

            return View(employees);
        }
    }
}
