// using Microsoft.AspNetCore.Mvc;
// using Microsoft.EntityFrameworkCore;
// using WebProject.Models;


// namespace WebProject.Controllers
// {
//     public class UserController : Controller
//     {

//         private readonly ApplicationDbContext context;

//         public UserController(ApplicationDbContext context)
//         {
//             this.context = context;
//         }




//         public async Task<IActionResult> Index()
//         {
//             var Users = await context.Users.ToListAsync();
//             return View(Users);
//         }

//         public async Task<IActionResult> GetOneUser(int id)
//         {
//             var User = await context.Users.FirstOrDefaultAsync(a => a.UserId == id);

//             if (User == null)
//             {
//                 return NotFound();
//             }

//             return View(User);
//         }

//         public IActionResult Create()
//         {
//             return View();
//         }

//         [HttpPost]
//         [ValidateAntiForgeryToken]
//         public async Task<IActionResult> Create(User user)
//         {
//             if (ModelState.IsValid)
//             {
//                 context.Users.Add(user);
//                 await context.SaveChangesAsync();
//                 TempData["SuccessMessage"] = "User created successfully!";
//                 return RedirectToAction(nameof(Index));
//             }
//             return View(user);
//         }





//         public async Task<IActionResult> Edit(int id)
//         {
//             var user = await context.Users.FindAsync(id);
//             if (user == null)
//             {
//                 return NotFound();
//             }
//             return View(user);
//         }

//         [HttpPost]
//         [ValidateAntiForgeryToken]
//         public async Task<IActionResult> Edit(User user)
//         {
//             if (ModelState.IsValid)
//             {
//                 context.Entry(user).State = EntityState.Modified;
//                 await context.SaveChangesAsync();
//                 TempData["SuccessMessage"] = "User updated successfully!";
//                 return RedirectToAction(nameof(Index));
//             }
//             return View(user);
//         }


//         public async Task<IActionResult> Delete(int id)
//         {
//             var user = await context.Users.FindAsync(id);
//             if (user == null)
//             {
//                 return NotFound();
//             }
//             return View(user);
//         }

//         [HttpPost, ActionName("Delete")]
//         [ValidateAntiForgeryToken]
//         public async Task<IActionResult> DeleteConfirmed(int id)
//         {
//             var user = await context.Users.FindAsync(id);

//             if (user != null)
//             {
//                 context.Users.Remove(user);
//                 await context.SaveChangesAsync();
//                 TempData["SuccessMessage"] = "User deleted successfully!";
//             }
//             return RedirectToAction(nameof(Index));
//         }



//     }

// }