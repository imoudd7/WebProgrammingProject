using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebProject.Models;


public class ApplicationDbContext : IdentityDbContext<User>
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

    public DbSet<Salons> Salons { get; set; } = null!;
    public DbSet<Service> Services { get; set; } = null!;
    public DbSet<Personal> Personaller { get; set; } = null!;
    public DbSet<Appointments> Appointments { get; set; } = null!;
    public DbSet<PersonalAvailabilities> Personal_Calisma_Zamanlari { get; set; } = null!;
    public override DbSet<User> Users { get; set; } = null!;

}