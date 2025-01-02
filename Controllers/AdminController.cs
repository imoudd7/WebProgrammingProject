using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebProject.Models;

namespace WebProject.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }
        [Authorize(Roles = "Admin")]
        public IActionResult AdminDashboard()
        {

            var employees = _context.Personals.ToList();

            var AppointmentTotal = _context.Appointments.Count();
            ViewData["AppointmentTotal"] = AppointmentTotal;

            return View(employees);
        }
    }
}
