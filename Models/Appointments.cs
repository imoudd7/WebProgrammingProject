using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace WebProject.Models;

public class Appointments
{
    [Key] public int Id { get; set; }

    public DateTime AppointmentTime { get; set; }
    public bool Onay { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal Ucret { get; set; }
}


