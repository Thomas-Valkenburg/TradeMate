using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models;

[Table(nameof(Inventory))]
public class Inventory
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; init; }

    [StringLength(255)]
    public required string Name { get; set; }

    [ForeignKey("CustomerId")]
    public virtual required Customer Customer { get; init; }

    public virtual List<StockItem> StockItems { get; } = [];

    public virtual List<Category> Categories { get; } = [];
}