using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebProject.Models;

public class Service
{
    [Key] public int ServiceId { get; set; }
    public string Name { get; set; } = string.Empty;
    public TimeSpan Duration { get; set; }
    [Column(TypeName = "decimal(18,2)")]
    public decimal Price { get; set; }
}
