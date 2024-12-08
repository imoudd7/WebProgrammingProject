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
    public DbSet<Salons>? Salonlar { get; set; }
    public DbSet<Services>? Servisler { get; set; }
    public DbSet<Personal>? Personaller { get; set; }
    public DbSet<PersonalServices>? Personal_servisleri { get; set; }
    public DbSet<PersonalAvailabilities>? Personal_Calisma_Zamanlari { get; set; }
    public DbSet<Appointments>? Appointments { get; set; }

}
