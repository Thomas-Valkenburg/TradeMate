using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models;

[Table(nameof(StockItem))]
public class StockItem
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; init; }

    public required int Barcode { get; set; }

    [StringLength(255)]
    public required string Name { get; set; }

    [ForeignKey("InventoryId")]
    public virtual required Inventory Inventory { get; init; }

    public virtual List<Category> Categories { get; init; } = [];
}