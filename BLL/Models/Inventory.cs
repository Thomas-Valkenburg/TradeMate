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
        var result = CheckIfValid(name);

        if (!result.Success) return result;
        
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
        
        var valid = StockItem.CheckIfValid(stockItem);
        
        if (!valid.Success) return valid;
        
        var result = _service.CreateStockItem(stockItem);

        return result;
    }

    internal Result CheckIfValid(string name) => CheckIfValid(name, Customer);
    
    internal static Result CheckIfValid(Inventory inventory) => CheckIfValid(inventory.Name, inventory.Customer);

    internal static Result CheckIfValid(string name, Domain.Models.Customer customer)
    {
        if (string.IsNullOrWhiteSpace(name)) return Result.FromError(ErrorType.Null, "Name can not be empty", "Name");
        if (name.Length > 30) return Result.FromError(ErrorType.TooLong, "Name can not be longer than 30 characters", "Name");
        if (customer.Inventories.Any(inventory => string.Equals(inventory.Name, name, StringComparison.CurrentCultureIgnoreCase)))
            return Result.FromError(ErrorType.Duplicate, $"There already exists an inventory with the name '{name}'",
                "Inventories.Name");

        return Result.FromSuccess();
    }

    internal static Inventory ConvertToBll(Domain.Models.Inventory data, IDal service) => new(service)
        {
        Id = data.Id,
        Name = data.Name,
        Customer = data.Customer,
            Categories = data.Categories,
            StockItems = data.StockItems
        };
    }