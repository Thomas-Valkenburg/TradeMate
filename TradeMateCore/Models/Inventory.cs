using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TradeMateCore.Models;

public class Inventory(string name)
{
    #region Attributes

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; init; }

    [StringLength(255)] 
    public string Name { get; private set; } = name;

    protected virtual List<StockItem> StockItems { get; } = [];

    #endregion

    #region Functions

    public void ChangeName(string name) => Name = name;

    public void AddStockItem(StockItem stockItem) 
    {
        StockItems.Add(stockItem);
    }

    public List<StockItem> GetStockItems(IEnumerable<Category> categories) => categories.SelectMany(targetCategory =>
        StockItems.FindAll(stockItem => stockItem.Categories.Any(category => category == targetCategory))).ToList();

    public static List<Inventory> GetAllInventories()
    {
        return [.. Database.GetContext().Inventories.ToList()];
    }

    #endregion
}