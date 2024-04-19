using DAL_Factory;
using Interfaces;

namespace BLL.Models;

public class Customer(Factory.ServiceType serviceType) : Domain.Models.Customer
{
    private readonly IDal _service = Factory.GetService(serviceType);

    public Result ChangeName(string name)
    {
        Name = name;
        
        return SaveCustomer();
    }

    public Result ChangeEmail(string email)
    {
        Email = email;
        
        return SaveCustomer();
    }
    
    public Result SaveCustomer()
    {
        return _service.CreateCustomer(this);
    }

    public List<Inventory> GetInventories()
    {
        var list = new List<Inventory>();

        _service.GetAllInventories(Id).ForEach(inventory =>
        {
            list.Add(Models.Inventory.ConvertToBll(inventory, _service));
        });

        return list;
    }

    public Result AddInventory(string name)
    {
        if (string.IsNullOrWhiteSpace(name) || name.Length > 30
                                            || Inventory.Any(inventory => string.Equals(inventory.Name, name,
                                                StringComparison.CurrentCultureIgnoreCase)))
            return Result.FromError(ErrorType.Duplicate, $"There already exists an inventory with the name '{name}'",
                "Inventory.Name");

        var inventory = new Inventory(_service)
        {
            Name = name,
            Customer = this
        };

        Inventory.Add(inventory);

        return _service.CreateInventory(inventory);
    }
}