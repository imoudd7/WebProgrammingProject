using System.ComponentModel.DataAnnotations;

namespace WebProject.Models;

public class User
{
  [Key]
  [Required]
  public int UserId { get; set; }

  [Required(ErrorMessage = "First Name is required")]
  [Display(Name = "First Name")]
  public string FirstName { get; set; } = string.Empty;

  [Required(ErrorMessage = "Last Name is required")]
  [Display(Name = "Last Name")]
  public string LastName { get; set; } = string.Empty;

  [Required(ErrorMessage = "Email is required")]
  [EmailAddress(ErrorMessage = "Invalid email address")]
  [Display(Name = "Email Address")]
  public string Email { get; set; } = string.Empty;

  [Required(ErrorMessage = "Password is required")]
  [DataType(DataType.Password)]
  [Display(Name = "Password")]
  public string Password { get; set; } = string.Empty;

  [Required]
  [Display(Name = "Role")]
  public UserRole Role { get; set; } = UserRole.Customer;
}
