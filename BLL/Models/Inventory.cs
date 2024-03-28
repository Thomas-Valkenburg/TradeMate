using Interfaces;

namespace BLL.Models;

public class Inventory : Domain.Models.Inventory
{
    public void ChangeName(string name) => Name = name;

    public void AddStockItem(StockItem stockItem)
    {
        StockItems.Add(stockItem);
    }

    public static List<Inventory> GetAllInventories()
    {
        throw new NotImplementedException();
    }
}