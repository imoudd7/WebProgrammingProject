using System.ComponentModel.DataAnnotations;
namespace WebProject.Models;


public class Salon
{
    [Key] public int SalonId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    public int ServiceId { get; set; }
    public Service? Service { get; set; }
}
