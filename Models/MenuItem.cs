using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Carlos_Pizza.Models;

public class MenuItem
{
    [Key]
    public int Id { get; set; }
    [StringLength(30)]
    public string Name { get; set; }
    [StringLength(255)]
    public string Desc { get; set; }
    public Nullable<bool> Available { get; set; }
    public bool? Vegetarian { get; set; }
    [DataType(DataType.Currency)]
    [Column(TypeName = "Money")]
    public Nullable<decimal> Price { get; set; }
}