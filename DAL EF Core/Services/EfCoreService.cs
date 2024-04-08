using Domain.Models;
using Interfaces;

namespace DAL_EF_Core.Services;

public class EfCoreService : IDal
{
    public List<Inventory>? GetAllInventories(int customerId)
    {
        throw new NotImplementedException();
    }

    public void CreateInventory(Inventory inventory)
    {
        throw new NotImplementedException();
    }

    public Customer? GetCustomer(int customerId)
    {
        throw new NotImplementedException();
    }

    public void SaveCustomer(Customer customer)
    {
        throw new NotImplementedException();
    }
}