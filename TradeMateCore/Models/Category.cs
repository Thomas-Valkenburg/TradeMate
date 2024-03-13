using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TradeMateCore.Models;

[Table(nameof(Category))]
public class Category
{
    [Key]
    public required int Id { get; set; }

    [StringLength(255)]
    public required string Name { get; set; }

    public virtual List<StockItem> StockItems { get; set; } = [];
}