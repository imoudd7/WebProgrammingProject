// using Microsoft.AspNetCore.Mvc;
// using Microsoft.EntityFrameworkCore;
// using WebProject.Models;

// namespace WebProject.Controllers
// {
//     [Route("api/[controller]")]
//     [ApiController]
//     public class UserApiController : ControllerBase
//     {
//         private readonly ApplicationDbContext context;

//         public UserApiController(ApplicationDbContext context)
//         {
//             this.context = context;
//         }





//         [HttpGet]
//         public List<User> Get()
//         {
//             return context.Users
//                 .Select(u => new User
//                 {
//                     UserId = u.UserId,
//                     FirstName = u.FirstName,
//                     LastName = u.LastName,
//                     Role = u.Role,
//                     Email = u.Email
//                 }).ToList();
//         }



//         [HttpGet("{id}")]
//         public ActionResult<User> Get(int id)
//         {
//             var user = context.Users.FirstOrDefault(u => u.UserId == id);
//             if (user is null)
//             {
//                 return NoContent();
//             }
//             return user;
//         }



//         [HttpPost]
//         public ActionResult Post([FromBody] User user)
//         {
//             context.Users.Add(user);
//             context.SaveChanges();
//             return Ok(user.FirstName + " " + user.LastName + " has been added");
//         }





//         [HttpPut("{id}")]
//         public ActionResult Put(int id, [FromBody] User user)
//         {
//             var existingUser = context.Users.FirstOrDefault(u => u.UserId == id);
//             if (existingUser is null)
//             {
//                 return NotFound();
//             }

//             existingUser.FirstName = user.FirstName;
//             existingUser.LastName = user.LastName;
//             existingUser.Role = user.Role;
//             existingUser.Email = user.Email;

//             context.Users.Update(existingUser);
//             context.SaveChanges();
//             return Ok(user.FirstName + " " + user.LastName + " has been updated");
//         }





//         [HttpDelete("{id}")]
//         public ActionResult Delete(int id)
//         {
//             var user = context.Users.FirstOrDefault(u => u.UserId == id);
//             if (user is null)
//             {
//                 return NotFound();
//             }

//             context.Users.Remove(user);
//             context.SaveChanges();
//             return Ok();
//         }
//     }
// }