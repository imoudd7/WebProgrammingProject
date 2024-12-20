using System.ComponentModel.DataAnnotations;
namespace WebProject.Models;


public class Salons
{
    [Key] public int SalonId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public TimeSpan OpeningTime { get; set; }
    public TimeSpan ClosingTime { get; set; }

}
