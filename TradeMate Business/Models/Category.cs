using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TradeMate_Business.Models;

[Table(nameof(Category))]
public class Category
{
    public Category() { }

    public Category(Inventory inventory)
    {
        Inventory = inventory;
    }
    
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; init; }

    [StringLength(30)] 
    public string Name { get; set; }

    [ForeignKey("InventoryId")]
    public virtual Inventory Inventory { get; init; }

    public virtual List<StockItem> StockItems { get; } = [];
}