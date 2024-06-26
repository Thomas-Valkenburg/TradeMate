﻿using DAL_Factory;
using Domain;
using Interfaces;

namespace BLL.Models;

public class Inventory : Domain.Models.Inventory
{
    public Inventory(Factory.ServiceType serviceType)
    {
        _service = Factory.GetDataService(serviceType);
    }

    internal Inventory(IDataAccessLayer service)
    {
        _service = service;
    }

    private readonly IDataAccessLayer _service;
    
    public Result ChangeName(string name)
    {
        var result = CheckIfValid(name);

        if (!result.Success) return result;
        
        Name = name;
        return Save();
    }
    
    private Result Save() => _service.UpdateInventory(this);

    public Result Delete()
    {
        Customer.Inventories.Remove(this);
        
        return _service.DeleteInventory(this);
    }

    public List<StockItem> GetStockItems()
    {
        var list = new List<StockItem>();

        _service.GetAllStockItems(Id).ForEach(stockItem =>
        {
            list.Add(StockItem.Convert(stockItem, _service));
        });

        return list;
    }

    public Result AddStockItem(string name, string barcode, int amount, decimal price)
    {
        var stockItem = new StockItem(_service)
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

    public static Result<Inventory?> TryGetInventory(int inventoryId, Factory.ServiceType serviceType)
    {
	    var service = Factory.GetDataService(serviceType);
	    var result = service.GetInventory(inventoryId);

	    if (!result.Success || result.Value is null) return Result.FromError<Inventory?>(result.Error ?? ErrorType.Unknown, result.ErrorMessage, result.ErrorPropertyName);

	    return Result.FromSuccess(Convert(result.Value, service))!;
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

    internal static Inventory Convert(Domain.Models.Inventory data, IDataAccessLayer service) => new(service)
    {
        Id = data.Id,
        Name = data.Name,
        Customer = data.Customer,
        Categories = data.Categories,
        StockItems = data.StockItems
    };
}