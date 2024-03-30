using DAL_Sqlite.Gateways;
using Domain.Models;
using Interfaces;

namespace DAL_Factory;

public class InventoryService : IInventory
{
    public List<Inventory> GetAllInventories(int customerId)
    {
        return new InventoryGateway().GetAllInventories(customerId).GetAwaiter().GetResult();
    }
}