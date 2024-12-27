using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProject.Models;

namespace WebProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceApiController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public ServiceApiController(ApplicationDbContext context)
        {
            this.context = context;
        }





        [HttpGet]
        public List<Service> Get()
        {
            var services = context.Services
                .Select(s => new Service
                {
                    ServiceId = s.ServiceId,
                    Name = s.Name,
                    Description = s.Description,
                    Duration = s.Duration,
                    Price = s.Price,
                    PersonalId = s.PersonalId,
                    Personal = s.Personal
                }).ToList();

            return services;
        }




        [HttpGet("{id}")]
        public ActionResult<Service> Get(int id)
        {
            var service = context.Services.FirstOrDefault(s => s.ServiceId == id);
            if (service is null)
            {
                return NoContent();
            }
            return service;
        }





        [HttpPost]
        public ActionResult Post([FromBody] Service service)
        {
            context.Services.Add(service);
            context.SaveChanges();
            return Ok(service.Name + " has been added.");
        }






        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Service service)
        {
            var existingService = context.Services.FirstOrDefault(s => s.ServiceId == id);
            if (existingService is null)
            {
                return NotFound();
            }

            existingService.Name = service.Name;
            context.Services.Update(existingService);
            context.SaveChanges();
            return Ok(service.Name + " has been updated.");
        }






        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var service = context.Services.FirstOrDefault(s => s.ServiceId == id);
            if (service is null)
            {
                return NotFound();
            }

            context.Services.Remove(service);
            context.SaveChanges();
            return Ok("Service has been deleted.");
        }
    }
}