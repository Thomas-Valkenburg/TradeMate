using System.ComponentModel.DataAnnotations;

namespace TradeMateCore.Models;

public class Inventory
{
    #region Attributes

    public int Id { get; init; }

    public string Name { get; private set; }

    private List<StockItem> StockItems { get; } = [];

    #endregion

    #region Functions

    public void ChangeName(string name) => Name = name;

    public void AddStockItem(StockItem stockItem) => StockItems.Add(stockItem);

    public List<StockItem> GetStockItems(IEnumerable<Category> categories) => categories.SelectMany(targetCategory =>
        StockItems.FindAll(stockItem => stockItem.Categories.Any(category => category == targetCategory))).ToList();

    #endregion
}