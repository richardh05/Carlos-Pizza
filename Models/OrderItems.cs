using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Carlos_Pizza.Models;

[PrimaryKey(nameof(OrderId), nameof(StockId))]
public class OrderItems
{
    [Required]
    public int OrderId { get; set; }
    [Required]
    public int StockId { get; set; }
    [Required]
    public int Quantity { get; set; }
}