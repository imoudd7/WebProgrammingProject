namespace WebProject.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Personal
{
    [Key] public int PersonalID { get; set; }
    [Required] public string? Ad { get; set; }
    [Required] public string? Soyad { get; set; }
    [Required] public string? Uzmanlik { get; set; }
    [Required][ForeignKey("Salons")] public int SalonId { get; set; }
    [Required] public Salons? Salon { get; set; }
    [Required] public ICollection<PersonalServices>? Personal_Servisler { get; set; }
    [Required] public ICollection<PersonalAvailabilities>? Personal_Zamanlari { get; set; }
}