namespace WebProject.Models;
using System.ComponentModel.DataAnnotations;
public class Personal
{
    [Key] public int Id { get; set; }
    public string? Ad { get; set; }
    public string? Soyad { get; set; }
    public string? Uzmanlık { get; set; }
    public int SalonId { get; set; } // Foreign key
    public Salons? Salon { get; set; }
    public required ICollection<PersonalServices> Personal_Servisler { get; set; } // Services the employee can perform
    public required ICollection<PersonalAvailabilities> Personal_Zamanlari { get; set; }
}