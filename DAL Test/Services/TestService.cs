using Domain.Models;
using Interfaces;

namespace DAL_Test.Services;

public class TestService : IDal
{
    #region Inventory

    public List<Inventory> GetAllInventories(int customerId)
    {
        if (customerId == 1)
        {
            return
            [
                new Inventory
                {
                    Id = 1,
                    Name = "Test Inventory",
                    Customer = new Customer
                    {
                        Id = 1,
                        Name = "User 1",
                        Email = "user@trademate.com"
                    }
                }
            ];
        }

        return [];
    }

    public void CreateInventory(Inventory inventory)
    {
        throw new NotImplementedException();
    }

    #endregion

    #region Customer

    public Customer? GetCustomer(int customerId)
    {
        throw new NotImplementedException();
    }

    public void SaveCustomer(Customer customer)
    {
        throw new NotImplementedException();
    }

    #endregion
}