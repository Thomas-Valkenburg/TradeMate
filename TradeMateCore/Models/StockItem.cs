using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TradeMateCore.Models;

[Table(nameof(StockItem))]
public class StockItem
{
    public StockItem() { }
    
    public StockItem(int id, string name)
    {
        Id         = id;
        Name       = name;
    }

    public StockItem(int id, string name, List<Category> categories)
    {
        Id   = id;
        Name = name;
        
        categories.ForEach(Categories.Add);
    }

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int Id { get; set; }

    [StringLength(255)]
    public string Name { get; set; }

    [ForeignKey("InventoryId")]
    public virtual Inventory Inventory { get; set; }

    public virtual List<Category> Categories { get; init; } = [];
}