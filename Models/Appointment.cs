using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace WebProject.Models;

public class Appointments
{
    [Key]
    public int Id { get; set; }

    [Required]
    public DateTime AppointmentTime { get; set; }

    [Required]
    public bool Onay { get; set; }

    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal Ucret { get; set; }

    [Required]
    public int PersonalId { get; set; }
    public Personal Personal { get; set; }

    [Required]
    public int UserId { get; set; }
    public User User { get; set; }

    [Required]
    public int ServiceId { get; set; }
    public Service Service { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}


