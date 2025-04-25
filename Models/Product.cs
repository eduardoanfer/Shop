using System.ComponentModel.DataAnnotations;

namespace Shop.Models;
//using System.Data.Anotations
public class Product
{
    [Key]

    public int Id { get; set; }
    [Required(ErrorMessage = "o nome deve ser preenchido")]
    [MaxLength(60,ErrorMessage = "deve conter no maximo 3 a 60 caracteres")]
    [MinLength(30,ErrorMessage = "deve conter no maximo 3 caracteres")]
    public string ProductName { get; set; }
    [MaxLength(1024,ErrorMessage = "Esse Campo deve ser Preenchido")]
    public string ProductDescription { get; set; }
    [MaxLength(ErrorMessage = "Esse Campo é obrigatorio")]
    [Range(1,int.MaxValue, ErrorMessage = "Categoria invalida")]
    public decimal ProductPrice { get; set; }
    [MaxLength(ErrorMessage = "Esse Campo é obrigatorio")]
    [Range(1,int.MaxValue, ErrorMessage = "Categoria invalida")]
    public int CategoryId { get; set; } // so a referência da categoria
    public Category Category { get; set; } // saber mais detalhes da categoria informaçoes do objeto dentro do outro. 

}
