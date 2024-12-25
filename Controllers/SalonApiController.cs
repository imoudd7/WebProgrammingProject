using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProject.Models;

namespace WebProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class SalonApiController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public SalonApiController(ApplicationDbContext context)
        {
            this.context = context;
        }



        [HttpGet]

        public List<Salon> Get()
        {
            var Salons = context.Salons
            .Select(s => new Salon 
            {
                SalonId = s.SalonId,
                Name = s.Name 
            }).ToList();
            return Salons;
        }



        [HttpGet("{id}")]
        public ActionResult<Salon> Get(int id)
        {
            var salon = context.Salons.FirstOrDefault(s => s.SalonId == id);
            if (salon is null)
            {
                return NoContent();
            }

            return salon;
        }



       [HttpPost]
        public ActionResult Post([FromBody] Salon salon)
        {
            context.Salons.Add(salon);
            context.SaveChanges();
            return Ok(salon.Name + " has been added.");
        }








    [HttpPut("{id}")]
    public ActionResult Put(int id, [FromBody] Salon salon)
    {
        var existingSalon = context.Salons.FirstOrDefault(s => s.SalonId == id);
        if (existingSalon is null)
        {
            return NotFound("Salon not found.");
        }

        existingSalon.Name = salon.Name;
        existingSalon.Description = salon.Description;
        existingSalon.ServiceId = salon.ServiceId;
   

        context.Salons.Update(existingSalon);
        context.SaveChanges();
        return Ok(salon.Name + " has been updated.");
    }

    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
        var salon = context.Salons.FirstOrDefault(s => s.SalonId == id);
        if (salon is null)
        {
            return NotFound("Salon not found.");
        }

        context.Salons.Remove(salon);
        context.SaveChanges();
        return Ok("Salon has been deleted.");
    }

    }

}