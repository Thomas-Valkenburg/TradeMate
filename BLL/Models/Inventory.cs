using DAL_Factory;
using Interfaces;

namespace BLL.Models;

public class Inventory : Domain.Models.Inventory
{
    public Inventory(Factory.ServiceType serviceType)
    {
        _service = Factory.GetService(serviceType);
    }
    
    internal Inventory(IDal service)
    {
        _service = service;
    }

    private readonly IDal _service;
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