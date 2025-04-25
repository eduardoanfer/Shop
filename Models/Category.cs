using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shop.Models
{
    [Table("Categoria")] // gerar uma tabela com nome categoria com Schema
    public class Category
    {
        [Key]
        [Column]
        public int Id { get; set; }

        [Required(ErrorMessage = "Esse Campo é Obrigatório")]
        [MaxLength(60, ErrorMessage = "Esse Campo deve conter entre 3 e 60 caracteres")]
        [MinLength(3, ErrorMessage = "Esse Campo deve conter entre 3 e 60 caracteres")]

        public string Title { get; set; }

    }
}