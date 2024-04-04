namespace DAL_Sqlite.Gateways;

public class InventoryService : Interfaces.IInventory
{
    public List<Domain.Models.Inventory> GetAllInventories(int customerId)
    {
        var inventoryList = Query.Execute<Data_Access_Models.Inventory>($"SELECT * FROM Inventory WHERE CustomerId={customerId}").GetAwaiter().GetResult();

        return Data_Access_Models.Inventory.ConvertToDomainClass(inventoryList);
    }
}