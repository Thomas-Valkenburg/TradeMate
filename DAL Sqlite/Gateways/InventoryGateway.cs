namespace DAL_Sqlite.Gateways;

public class InventoryGateway
{
    public async Task<List<Domain.Models.Inventory>> GetAllInventories(int customerId)
    {
        var inventoryList = await Query.Execute<Data_Access_Models.Inventory>($"SELECT * FROM Inventory WHERE CustomerId={customerId}");

        return Data_Access_Models.Inventory.ConvertToDomainClass(inventoryList);
    }
}