using Microsoft.EntityFrameworkCore;
using WebProject.Models;
using WebProject.Controllers;


internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));



        // Add services to the container.
        builder.Services.AddControllersWithViews();
        builder.Services.AddSession();//session ekleme
                                      // HttpContext.Session.SetString("Admin", "true"); to be added at login action result and in the admin controller to check HttpContext.Session.GetString("Admin") != "true"


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


        app.Run();
        app.UseSession();













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