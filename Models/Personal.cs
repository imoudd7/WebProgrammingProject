namespace WebProject.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Personal
{
    [Key] public int PersonalID { get; set; }

    [Required] public string Ad { get; set; } = string.Empty;
    [Required] public string Soyad { get; set; } = string.Empty;
    [Required] public string Uzmanlik { get; set; } = string.Empty;

    [Required]




    public ICollection<Services> Personal_Servisler { get; set; } = new List<Services>();
    public ICollection<PersonalAvailabilities> Personal_Zamanlari { get; set; } = new List<PersonalAvailabilities>();
}

