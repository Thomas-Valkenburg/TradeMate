using DAL_Factory;

namespace BLL.Models;

public class Inventory : Domain.Models.Inventory
{
    private static readonly InventoryService InventoryService = new();
    
    public void ChangeName(string name) => Name = name;

    public void AddStockItem(StockItem stockItem)
    {
        StockItems.Add(stockItem);
    }

    public static List<Inventory> GetAllInventories(int customerId)
    {
        var list = new List<Inventory>();
        
        InventoryService.GetAllInventories(customerId).ForEach(inventory =>
        {
            list.Add(ConvertDomainToBll(inventory));
        });

        return list;
    }

    private static Inventory ConvertDomainToBll(Domain.Models.Inventory data)
    {
        return new Inventory
        {
            Id         = data.Id,
            Name       = data.Name,
            Customer   = data.Customer,
            Categories = data.Categories,
            StockItems = data.StockItems
        };
    }
}