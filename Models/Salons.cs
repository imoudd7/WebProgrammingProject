using System.ComponentModel.DataAnnotations;
namespace WebProject.Models;


public class Salons
{
    [Key] public int SalonId { get; set; }
    public string? Name { get; set; }
    public string? Address { get; set; }
    public string? PhoneNumber { get; set; }
    public TimeSpan OpeningTime { get; set; }
    public TimeSpan ClosingTime { get; set; }
    public ICollection<Personal>? Personals { get; set; }
}