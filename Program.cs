using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using WebProject.Models;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // إعداد قاعدة البيانات
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
        var hasher = new PasswordHasher<User>();
        string password = "sau";
        string hashedPassword = hasher.HashPassword(null, password);

        // Print the plain and hashed passwords to the console
        Console.WriteLine($"Plain Password: {password}");
        Console.WriteLine($"Hashed Password: {hashedPassword}");
        // إعداد Identity
        builder.Services.AddIdentity<User, IdentityRole>()
            .AddRoles<IdentityRole>()
            .AddDefaultTokenProviders()
            .AddDefaultUI()
            .AddEntityFrameworkStores<ApplicationDbContext>();

        builder.Services.Configure<IdentityOptions>(options =>
        {
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = true;
            options.Password.RequiredLength = 5;

            options.Lockout.MaxFailedAccessAttempts = 3;
        });

        // تسجيل IHttpClientFactory
        builder.Services.AddHttpClient();

        // تسجيل الخدمات الأخرى
        builder.Services.AddSession(); // لتفعيل الـ Session
        builder.Services.AddControllersWithViews();
        builder.Services.AddRazorPages();

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
        app.UseSession(); // لتفعيل جلسات العمل

        // إعداد المسارات
        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.MapRazorPages();

        app.Run();
    }
}
