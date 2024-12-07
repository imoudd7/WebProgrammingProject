using System.ComponentModel.DataAnnotations;

namespace WebProject.Models;
public class PersonalAvailabilities
{
    [Key] public int Id { get; set; }
    public int EmployeeId { get; set; }
    [Required] public required Personal Personal { get; set; }
    public DayOfWeek DayOfWeek { get; set; }
    public TimeSpan OpeningTime { get; set; }
    public TimeSpan ClosingTime { get; set; }
}
