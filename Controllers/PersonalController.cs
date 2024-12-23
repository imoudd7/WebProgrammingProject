using Microsoft.AspNetCore.Mvc;
using WebProject.Models;
namespace WebProject.Controllers;



public class PersonalController : Controller
{

    private readonly ApplicationDbContext _context;
    public PersonalController(ApplicationDbContext context)
    {
        _context = context;

        var Personals = new List<Personal>
        {
            new Personal
            {
                Ad="ahmed",
                Soyad="bahaa",
                Uzmanlik={},



            },
             new Personal
            {
                  Ad="mahmoud",
                Soyad="aldaher",
                Uzmanlik={},

            },
             new Personal
            {
                  Ad="umut",
                Soyad="bulut",
                Uzmanlik={},

            },
             new Personal
            {
                  Ad="can",
                Soyad="bulat",
                Uzmanlik={},


            },
             new Personal
            {
                  Ad="yusuf",
                Soyad="akinci",
                Uzmanlik={},

            }


        };
        context.Personaller.AddRange(Personals);
        _context.SaveChanges();

    }

    public ActionResult Person()
    {
        return View();
    }
}