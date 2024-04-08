using Domain.Models;
using Interfaces;

namespace DAL_Sqlite.Services;

public class SqLiteService : IDal
{
    #region Inventory

    public List<Domain.Models.Inventory>? GetAllInventories(int customerId)
    {
        var inventoryList = Query.ExecuteReadMany<Data_Access_Models.Inventory>($"SELECT * FROM Inventory WHERE CustomerId={customerId}").GetAwaiter().GetResult();

        if (inventoryList == null || inventoryList.Count < 1)
        {
            return null;
        }

        var domainInventoriesList = new List<Domain.Models.Inventory>();
        
        inventoryList.ForEach(inventory => domainInventoriesList.Add(inventory.ConvertToDomainClass()!));
        
        return domainInventoriesList;
    }

    public void CreateInventory(Domain.Models.Inventory inventory)
    {
        throw new NotImplementedException();
    }

    #endregion

    #region Customer

    public Domain.Models.Customer? GetCustomer(int customerId)
    {
        var customer = Query.ExecuteReadFirst<Data_Access_Models.Customer>($"SELECT * FROM Customer WHERE Id={customerId}").GetAwaiter().GetResult();
        
        throw new NotImplementedException();
    }

    public void SaveCustomer(Customer customer)
    {
        throw new NotImplementedException();
    }

    #endregion
}