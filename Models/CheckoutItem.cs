using System.ComponentModel.DataAnnotations;

namespace Carlos_Pizza.Models;

public class CheckoutItem
{
    [Key,Required]
    public int Id { get; set; }
    [Required]
    public decimal Price { get; set; }
    [Required,StringLength(50)]
    public string Name { get; set; }
    [Required]
    public int Quantity { get; set; }
}