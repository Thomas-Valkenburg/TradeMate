using DAL_Factory;
using Domain;
using Interfaces;

namespace BLL.Models;

public class StockItem : Domain.Models.StockItem
{
    private readonly IDataAccessLayer _service;
    
    public StockItem(string name, string barcode, int amount, decimal price, Inventory inventory, Factory.ServiceType serviceType)
    {
        Barcode = barcode;
        Name = name;
        Amount = amount;
        Price = price;
        Inventory = inventory;
        _service = Factory.GetDataService(serviceType);
    }

    public StockItem(string name, string barcode, int amount, decimal price, Inventory inventory, IDataAccessLayer service)
    {
	    Barcode = barcode;
	    Name = name;
	    Amount = amount;
	    Price = price;
	    Inventory = inventory;
		_service = service;
    }

    public StockItem(string name, string barcode, int amount, decimal price, Domain.Models.Inventory inventory, Factory.ServiceType serviceType)
    {
	    Barcode = barcode;
	    Name = name;
	    Amount = amount;
	    Price = price;
	    Inventory = inventory;
	    _service = Factory.GetDataService(serviceType);
    }

    public StockItem(string name, string barcode, int amount, decimal price, Domain.Models.Inventory inventory, IDataAccessLayer service)
    {
	    Barcode = barcode;
	    Name = name;
	    Amount = amount;
	    Price = price;
	    Inventory = inventory;
	    _service = service;
    }

	public Result ChangeName(string newName)
    {
        var result = CheckIfValid(newName);

        if (!result.Success) return result;
        
        Name = newName;
        return Save();

    }

    public Result ChangeBarcode(string newBarcode)
    {
        var oldBarcode = Barcode;
        Barcode = newBarcode;

        var result = Save();
        
        if (!result.Success) Barcode = oldBarcode;
        
        return result;
    }
    
    public Result ChangeAmount(int newAmount)
    {
        var result = CheckIfValid(newAmount);
        
        if (!result.Success) return result;
        
        Amount = newAmount;
        return Save();
    }

    public Result ChangePrice(decimal newPrice)
    {
        var result = CheckIfValid(newPrice);
        
        if (!result.Success) return result;
        
        Price = newPrice;
        return Save();
    }
    
    private Result Save() => _service.UpdateStockItem(this);
    
    public Result Delete() => _service.DeleteStockItem(this);

    public static Result<StockItem?> TryGetStockItem(int stockItemId, Factory.ServiceType serviceType)
    {
	    var service = Factory.GetDataService(serviceType);
	    var result  = service.GetStockItem(stockItemId);

	    if (!result.Success || result.Value is null) return Result.FromError<StockItem>(result.Error ?? ErrorType.Unknown, result.ErrorMessage, result.ErrorPropertyName);

	    return Result.FromSuccess(Convert(result.Value, service))!;
    }

    private Result CheckIfValid(string name) => CheckIfValid(name, Amount, Price);
    private Result CheckIfValid(int amount) => CheckIfValid(Name, amount, Price);
    private Result CheckIfValid(decimal price) => CheckIfValid(Name, Amount, price);
    
    internal static Result CheckIfValid(StockItem stockItem) =>
        CheckIfValid(stockItem.Name, stockItem.Amount, stockItem.Price);
    
    internal static Result CheckIfValid(string name, int amount, decimal price)
    {
        if (string.IsNullOrWhiteSpace(name)) return Result.FromError(ErrorType.Null, "Name can not be empty", "Name");
        if (name.Length > 90) return Result.FromError(ErrorType.TooLong, "Name can not be longer than 90 characters", "Name");
        if (amount < 0) return Result.FromError(ErrorType.TooShort, "Amount can not be less than 0", "Amount");
        if (price < 0) return Result.FromError(ErrorType.TooShort, "Price can not be less than 0", "Price");
        
        return Result.FromSuccess();
    }

    internal static StockItem Convert(Domain.Models.StockItem stockItem, IDataAccessLayer service) => new(stockItem.Name, stockItem.Barcode, stockItem.Amount, stockItem.Price, stockItem.Inventory, service)
    {
        Id = stockItem.Id
    };
}