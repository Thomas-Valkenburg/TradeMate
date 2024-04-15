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
            list.Add(Models.Inventory.ConvertDomainToBll(inventory, _service));
        });

        return list;
    }
}