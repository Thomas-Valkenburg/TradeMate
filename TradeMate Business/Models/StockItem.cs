using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TradeMate_Business.Models;

[Table(nameof(StockItem))]
public class StockItem
{
    public StockItem() { }
    
    public StockItem(string name, Inventory inventory)
    {
        Name      = name;
        Inventory = inventory;
    }

    public StockItem(string name, Inventory inventory, List<Category> categories)
    {
        Name      = name;
        Inventory = inventory;
        
        categories.ForEach(Categories.Add);
    }

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; init; }

    public int Barcode { get; set; }

    [StringLength(255)]
    public string Name { get; set; }

    [ForeignKey("InventoryId")]
    public virtual Inventory Inventory { get; init; }

    public virtual List<Category> Categories { get; init; } = [];
}