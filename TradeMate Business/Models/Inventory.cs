using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TradeMate_Business.Models;

[Table(nameof(Inventory))]
public class Inventory
{
    public Inventory() { }

    public Inventory(string name, Customer customer)
    {
        Name     = name;
        Customer = customer;
    }

    #region Attributes

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; init; }

    [StringLength(255)]
    public string Name { get; private set; }

    [ForeignKey("CustomerId")]
    public virtual Customer Customer { get; init; }

    public virtual List<StockItem> StockItems { get; } = [];

    public virtual List<Category> Categories { get; } = [];

    #endregion

    #region Functions

    public void ChangeName(string name) => Name = name;

    public void AddStockItem(StockItem stockItem) 
    {
        StockItems.Add(stockItem);
    }

    #endregion
}