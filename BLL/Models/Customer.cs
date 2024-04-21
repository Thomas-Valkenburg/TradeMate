using DAL_Factory;
using Domain;
using Interfaces;

namespace BLL.Models;

public class Customer(Factory.ServiceType serviceType) : Domain.Models.Customer
{
    private readonly IDataAccessLayer _service = Factory.GetService(serviceType);

    public Result Save()
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
        var inventory = new Inventory(_service)
        {
            Name = name,
            Customer = this
        };

        var result = Inventory.CheckIfValid(inventory);
        if (!result.Success) return result;

        return _service.CreateInventory(inventory);
    }

	public static Customer? TryGetCustomer(int customerId, Factory.ServiceType serviceType)
	{
		var customer = Factory.GetDataService(serviceType).GetCustomer(customerId);

        return customer == null ? null : ConvertToBll(customer, serviceType);
	}

	internal static Customer ConvertToBll(Domain.Models.Customer customer, Factory.ServiceType serviceType)
    {
	    return new Customer(serviceType)
	    {
		    Id          = customer.Id,
		    Inventories = customer.Inventories
	    };
    }
}