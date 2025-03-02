using System.ComponentModel.DataAnnotations;

namespace Carlos_Pizza.Models;

public class OrderHistory
{
    [Key, Required]
    public int OrderNo { get; set; }
    [Required]
    public string Email { get; set; }
}