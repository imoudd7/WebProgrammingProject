using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;


namespace WebProject.Models;

public class User : IdentityUser
{


  [Display(Name = "First Name")]
  [Required]
  public string FirstName { get; set; } = string.Empty;


  [Display(Name = "Last Name")]
  [Required]
  public string LastName { get; set; } = string.Empty;
}
