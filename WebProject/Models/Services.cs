using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebProject.Models;

public class Services
{
    [Key] public int ServiceId { get; set; }
    public string? Name { get; set; }
    public TimeSpan Duration { get; set; }
    public decimal Price { get; set; }
    [ForeignKey("Salons")] public int SalonId { get; set; }
    public Salons? Salon { get; set; }
}