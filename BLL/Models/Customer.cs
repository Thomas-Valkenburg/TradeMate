using DAL_Factory;
using Interfaces;

namespace BLL.Models;

public class Customer(Factory.ServiceType serviceType) : Domain.Models.Customer
{
    private readonly IDal _service = Factory.GetService(serviceType);

    public Result SaveCustomer()
    {
        return _service.CreateCustomer(this);
    }
    
    public List<Inventory> GetAllInventories()
    {
        var list = new List<Inventory>();

        _service.GetAllInventories(Id).ForEach(inventory =>
        {
            list.Add(Models.Inventory.ConvertToBll(inventory, _service));
        });

        return list;
    }

    public bool AddInventory(string name)
    {
        if (string.IsNullOrWhiteSpace(name) || name.Length > 30 ||
            Inventory.Any(inventory => string.Equals(inventory.Name, name, StringComparison.CurrentCultureIgnoreCase))) return false;
        
        Inventory.Add(new Inventory(_service)
        {
            Name = name,
            Customer = this
        });
        
        // Save to database

        return true;
    }
}