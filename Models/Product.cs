using System.ComponentModel.DataAnnotations;

namespace Shop.Models;
//using System.Data.Anotations
public class Product
{
    [Key]

    public int ProductId { get; set; }
    [Required(ErrorMessage = "o nome deve ser preenchido")]
    [MaxLength(60,ErrorMessage = "deve conter no maximo 3 a 60 caracteres")]
    [MinLength(30,ErrorMessage = "deve conter no maximo 3 caracteres")]
    public string ProductName { get; set; }
    public string ProductDescription { get; set; }
    public decimal ProductPrice { get; set; }
    public string ProductImage { get; set; }

}
