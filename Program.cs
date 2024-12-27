using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using WebProject.Models;
using WebProject.Controllers;


internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

        var hasher = new PasswordHasher<User>();
        string password = "sau";
        string hashedPassword = hasher.HashPassword(null, password);

        // AQAAAAIAAYagAAAAELfeVTvHufd1nCbeQjl/Dy8nSdGB2B2iPbKZlYTJgPepUYKDu1/Dcmf32uFCbmiKUQ==

        // Add services to the container.

        builder.Services.AddSession();//session ekleme
                                      // HttpContext.Session.SetString("Admin", "true"); to be added at login action result and in the admin controller to check HttpContext.Session.GetString("Admin") != "true"

        builder.Services.AddIdentity<User, IdentityRole>()
        .AddRoles<IdentityRole>()
            .AddDefaultTokenProviders()
            .AddDefaultUI()

            .AddEntityFrameworkStores<ApplicationDbContext>();
        builder.Services.AddControllersWithViews();
        builder.Services.AddRazorPages();

        builder.Services.Configure<IdentityOptions>(options =>
        {
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = true;
            options.Password.RequiredLength = 5;

            options.Lockout.MaxFailedAccessAttempts = 3;
        });
        var app = builder.Build();

        // Configure the HTTP request pipeline
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }
        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();
        app.UseAuthentication();

        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");
        app.UseEndpoints(endpoints =>
        {
            _ = endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Register}/{action=Login}/{id?}");
        });



        app.UseSession();



        app.MapRazorPages();











        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();
        // Configure the HTTP request pipeline.
        app.MapControllers();
        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();
    }

}
