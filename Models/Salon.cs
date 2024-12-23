using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebProject.Models;

public class Salon
{
    [Key]
    public int SalonId { get; set; } 

    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty; 
    [MaxLength(250)]
    public string Description { get; set; } = string.Empty; 


    [ForeignKey("ResponsiblePersonnel")]
    public int PersonalId { get; set; }
    public Personal Personal { get; set; } = null!;
}