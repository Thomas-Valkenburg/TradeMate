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
    
    public Result ChangeName(string name)
    {
        Name = name;
        
        return _service.UpdateInventory(this);
    }

    public Result Delete()
    {
        Customer.Inventory.Remove(this);
        
        return _service.DeleteInventory(Id);
    }

    public Result AddStockItem(string name, int barcode, int amount, decimal price)
    {
        var stockItem = new StockItem
        {
            Name = name,
            Barcode = barcode,
            Amount = amount,
            Price = price,
            Inventory = this
        };
        
        StockItems.Add(stockItem);
        
        return _service.CreateStockItem(stockItem);
    }

    internal static Inventory ConvertToBll(Domain.Models.Inventory data, IDal service)
    {
        return new Inventory(service)
        {
            Id         = data.Id,
            Name       = data.Name,
            Customer   = data.Customer,
            Categories = data.Categories,
            StockItems = data.StockItems
        };
    }
}