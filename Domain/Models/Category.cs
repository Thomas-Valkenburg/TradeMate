using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models;

[Table(nameof(Category))]
public class Category
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; init; }

    [StringLength(30)] 
    public required string Name { get; set; }

    [ForeignKey("InventoryId")]
    public virtual required Inventory Inventory { get; init; }

    public virtual List<StockItem> StockItems { get; } = [];
}