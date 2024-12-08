using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace WebProject.Models;

public class Appointments
{
    [Key] public int Id { get; set; }
    [ForeignKey("Personal")] public int PersonalId { get; set; }
    [Required] public Personal? Person { get; set; }
    public int ServiceId { get; set; }
    [Required] public Services? Servisler { get; set; }
    public DateTime AppointmentTime { get; set; }
    public bool Onay { get; set; }
    public decimal Ucret { get; set; }
}

