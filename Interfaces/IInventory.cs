using Domain.Models;

namespace Interfaces;

public interface IInventory
{
    List<Inventory>? GetAllInventories(int customerId);
    
    void CreateInventory(Inventory inventory);
}