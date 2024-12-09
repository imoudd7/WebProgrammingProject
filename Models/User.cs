using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace WebProject.Models;

public class User
{

  [Key][Required] public int UserId { get; set; }

  [Required][Display(Name = "Ad")] public string? FirstName { get; set; }
  [Required][Display(Name = "Soyad")] public string? LastName { get; set; }
  [Required][EmailAddress(ErrorMessage = "Invalid email address")] public string? Email { get; set; }
  [Required][Display(Name = "Sifre")] public string? Password { get; set; }
  public string Role { get; set; } = "User";

}