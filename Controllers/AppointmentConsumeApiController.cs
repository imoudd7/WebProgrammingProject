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

        public async Task<IActionResult> GetAllAppointments()
        {
            List<Appointments> appointments = new List<Appointments>();

            try
            {
                using var httpClient = new HttpClient();
                var response = await httpClient.GetAsync("http://localhost:5180/api/AppointmentApi/");
                response.EnsureSuccessStatusCode();
                var jsonData = await response.Content.ReadAsStringAsync();
                appointments = JsonConvert.DeserializeObject<List<Appointments>>(jsonData);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "error " + ex.Message);
                return View(new List<Appointments>());
            }

            return View(appointments);
        }



    }
}
