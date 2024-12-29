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

    public DateTime CreatedAt { get; set; }

    [Required]
    public bool Onay { get; set; }


    [ForeignKey("Salon")]
    public int? SalonId { get; set; }
    [ForeignKey("Service")]
    public int? ServiceId { get; set; }
    [ForeignKey("Personal")]
    public int? PersonalID { get; set; }


    // public virtual Salon? Salon { get; set; }

    [ForeignKey("User")]
    public string? UserId { get; set; }
    // public virtual User? User { get; set; }



}