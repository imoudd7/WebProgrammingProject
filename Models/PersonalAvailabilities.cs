using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebProject.Models;
public class PersonalAvailabilities
{
    [Key] public int Id { get; set; }
    [ForeignKey("Personal")] public int PersonalID { get; set; }
    [Required] public Personal? Personal { get; set; }
    public DayOfWeek DayOfWeek { get; set; }
    public TimeSpan OpeningTime { get; set; }
    public TimeSpan ClosingTime { get; set; }
}
