using DAL_Factory;
using Interfaces;

namespace BLL.Models;

public class StockItem : Domain.Models.StockItem
{
    private readonly IDal _service;
    
    public StockItem(Factory.ServiceType serviceType)
    {
        _service = Factory.GetService(serviceType);
    }

    public StockItem(IDal service)
    {
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
}