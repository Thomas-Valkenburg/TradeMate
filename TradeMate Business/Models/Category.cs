using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

<<<<<<<< HEAD:Domain/Models/Category.cs
namespace Domain.Models;
========
namespace TradeMate_Business.Models;
>>>>>>>> 18a6114e3b9a14f2fd25ae9bde0ca0f20161e8ef:TradeMate Business/Models/Category.cs

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