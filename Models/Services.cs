using System.ComponentModel.DataAnnotations;

namespace WebProject.Models;

public class Services
{
    [Key] public int Id { get; set; }
    public string? Name { get; set; }
    public TimeSpan Duration { get; set; }
    public decimal Price { get; set; }
    public int SalonId { get; set; }
    public Salons? Salon { get; set; }
}