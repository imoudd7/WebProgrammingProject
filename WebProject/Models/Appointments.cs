using System.ComponentModel.DataAnnotations;

namespace WebProject.Models;

public class Appointments
{
    [Key] public int Id { get; set; }
    public int PersonalId { get; set; }
    [Required] public required Personal Employee { get; set; }
    public int ServiceId { get; set; }
    [Required] public required Services Servisler { get; set; }
    public DateTime AppointmentTime { get; set; }
    public bool Onay { get; set; }
    public decimal Ucret { get; set; }
}
