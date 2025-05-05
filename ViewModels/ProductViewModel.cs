using System.ComponentModel.DataAnnotations;

namespace Shop.ViewModels;

public class ProductViewModel
{
    [Required]
    public string Title { get; set; }

    public string Description { get; set; }

    [Required]
    public decimal Price { get; set; }

    [Required]
    public int CategoryId { get; set; }
}