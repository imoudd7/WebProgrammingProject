using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;


namespace WebProject.Models;

public class User : IdentityUser
{

  [Required(ErrorMessage = "First Name is required")]
  [Display(Name = "First Name")]
  public string FirstName { get; set; } = string.Empty;

  [Required(ErrorMessage = "Last Name is required")]
  [Display(Name = "Last Name")]
  public string LastName { get; set; } = string.Empty;
}
