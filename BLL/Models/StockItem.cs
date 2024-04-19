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
}