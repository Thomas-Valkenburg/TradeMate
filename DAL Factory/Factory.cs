using Interfaces;

namespace DAL_Factory;

public class Factory
{
    public static IInventory GetInventoryGateway()
    {
        return new DAL_Sqlite.Gateways.InventoryService();
    }
}