using BLL.Models;

namespace Web.Models;

public class InventoriesViewModel(List<Inventory> inventories)
{
    public List<Inventory> Inventories { get; } = inventories;
}