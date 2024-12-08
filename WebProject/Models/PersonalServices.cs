namespace WebProject.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
public class PersonalServices
{
    [Key] public int PersonalID { get; set; }
    public required Personal Personal { get; set; }
    [ForeignKey("Services")] public int ServiceId { get; set; }
    public required Services Servisler { get; set; }
}
