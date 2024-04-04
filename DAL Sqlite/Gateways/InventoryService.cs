using Interfaces;

namespace DAL_Sqlite.Gateways;

public class InventoryService : IInventory
{
    public List<Domain.Models.Inventory>? GetAllInventories(int customerId)
    {
        var inventoryList = Query.ExecuteReadMany<Data_Access_Models.Inventory>($"SELECT * FROM Inventory WHERE CustomerId={customerId}").GetAwaiter().GetResult();

        if (inventoryList == null || inventoryList.Count < 1)
        {
            return null;
        }
        
        return Data_Access_Models.Inventory.ConvertToDomainClass(inventoryList);
    }

    public void CreateInventory(Domain.Models.Inventory inventory)
    {
        throw new NotImplementedException();
    }
}