using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WebProject.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System;

namespace WebProject.Controllers
{
    public class AppointmentConsumeApiController : Controller
    {
        private readonly ApplicationDbContext _dataBase;

        public AppointmentConsumeApiController(ApplicationDbContext dataBase)
        {
            _dataBase = dataBase;
        }

        // استعراض جميع المواعيد من الـ API
        public async Task<IActionResult> GetAllAppointments()
        {
            List<Appointments> appointments = new List<Appointments>();

            try
            {
                using var httpClient = new HttpClient();
                var response = await httpClient.GetAsync("https://localhost:5180/api/AppointmentApi/");
                response.EnsureSuccessStatusCode();
                var jsonData = await response.Content.ReadAsStringAsync();
                appointments = JsonConvert.DeserializeObject<List<Appointments>>(jsonData);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "An error occurred while fetching the data: " + ex.Message);
                return View(new List<Appointments>());  
            }

            return View(appointments);  
        }

        // عرض تفاصيل الموعد
        public IActionResult GetOneAppointment(int id)
        {
            var appointment = _dataBase.Appointments.FirstOrDefault(a => a.AppointmentId == id);

            if (appointment == null)
            {
                return NotFound("The requested appointment was not found.");
            }

            return View(appointment);  
        }

    }
}
