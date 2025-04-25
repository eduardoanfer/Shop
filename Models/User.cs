using System.ComponentModel.DataAnnotations;

namespace Shop.Models;

public class User
{
    [Key]
    public int Id { get; set; }
    [Required(ErrorMessage = "o nome deve ser preenchido")]
    [MaxLength(20,ErrorMessage = "deve conter no maximo 3 a 60 caracteres")]
    [MinLength(3,ErrorMessage = "deve conter no maximo 3 a 60 caracteres")]
    public string Username { get; set; }
    [Required(ErrorMessage = "o nome deve ser preenchido")]
    [MaxLength(20,ErrorMessage = "deve conter no maximo 3 a 60 caracteres")]
    [MinLength(3,ErrorMessage = "deve conter no maximo 3 a 60 caracteres")]
    public string Password { get; set; }
    public string Role { get; set; }
}