using System.ComponentModel.DataAnnotations;

namespace WebProject.Models;


public class Admin
{

  [Key]
  [Required]
  public int AdminId { get; set; }


    [Required]
    [MaxLength(50)]
    public string Name { get; set; }

    [Required]
    [EmailAddress]
    [MaxLength(150)]
    public string Email { get; set; }


    [Required]
    [MinLength(6)]
    public string Password { get; set; }

}