using DAL_Factory;
using Domain;
using Interfaces;

namespace BLL.Models;

public class Customer(Factory.ServiceType serviceType) : Domain.Models.Customer
{
    private readonly IDataAccessLayer _service = Factory.GetDataService(serviceType);
    
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

	public static Result<Customer?> TryGetCustomer(int customerId, Factory.ServiceType serviceType)
	{
		var result = Factory.GetDataService(serviceType).GetCustomer(customerId);

		if (!result.Success || result.Value is null) return Result.FromError<Customer>(result.Error ?? ErrorType.Duplicate, result.ErrorMessage, result.ErrorPropertyName);

        return Result.FromSuccess(Convert(result.Value, serviceType))!;
	}

	private static Customer Convert(Domain.Models.Customer customer, Factory.ServiceType serviceType)
    {
	    return new Customer(serviceType)
	    {
		    Id          = customer.Id,
		    Inventories = customer.Inventories
	    };
    }
}