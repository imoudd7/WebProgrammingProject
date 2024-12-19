using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using WebProject.Models;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Server=localhost\MSSQLSERVER01;Database=Web_Project;Encrypt=True;Trusted_Connection=True;TrustServerCertificate=True;");

    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

    }





    public DbSet<Salons> Salonlar { get; set; } = null!;
    public DbSet<Service> Servisler { get; set; } = null!;
    public DbSet<Personal> Personaller { get; set; } = null!;
    public DbSet<Admin> Adminler { get; set; } = null!;
    public DbSet<PersonalAvailabilities> Personal_Calisma_Zamanlari { get; set; } = null!;
    public DbSet<Appointments> Appointments { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;

}