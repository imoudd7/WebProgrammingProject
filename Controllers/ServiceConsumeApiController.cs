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

public class ServiceConsumeApiController : Controller
{
    private readonly ApplicationDbContext _dataBase;

    public ServiceConsumeApiController(ApplicationDbContext dataBase)
    {
        _dataBase = dataBase;
    }

    // استعراض جميع الخدمات من الـ API
    public async Task<IActionResult> GetAllServices()
    {
        List<Service> services = new List<Service>();

        try
        {
            using var httpClient = new HttpClient();
            var response = await httpClient.GetAsync("https://localhost:5180/api/ServiceApi/");
            response.EnsureSuccessStatusCode();
            var jsonData = await response.Content.ReadAsStringAsync();
            services = JsonConvert.DeserializeObject<List<Service>>(jsonData);
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, "An error occurred while fetching the data: " + ex.Message);
            return View(new List<Service>());  
        }

        return View(services);  
    }

    // عرض تفاصيل الخدمة
    public ActionResult Details(int id)
    {
        var service = _dataBase.Services.FirstOrDefault(a => a.ServiceId == id); // تغيير ServiceId إلى Id

        if (service == null)
        {
            return NotFound("The requested service was not found.");
        }

        return View(service);  
    }

}
