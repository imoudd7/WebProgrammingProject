using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace WebProject.Models;

public class Appointments
{
    [Key]
    public int AppointmentId { get; set; } 

    [Required]
    public DateTime AppointmentTime { get; set; }

    [Required]

    public DateTime CreatedAt {get;set;}

    [Required]
    public bool Onay { get; set; }

    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal Ucret { get; set; }

    [Required]
    public int SalonId { get; set; }
    public Salon Salon { get; set; }

    [Required]
    public int UserId { get; set; }
    public User User { get; set; }

}