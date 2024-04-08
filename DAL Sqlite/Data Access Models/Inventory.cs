using DAL_Sqlite.Services;

namespace DAL_Sqlite.Data_Access_Models;

internal class Inventory
{
    internal required int Id { get; init; }
    
    internal required string Name { get; init; }
    
    internal required int CustomerId { get; init; }

    internal Domain.Models.Inventory? ConvertToDomainClass()
    {
        var customer = new SqLiteService().GetCustomer(CustomerId);

        if (customer == null) return null;
        
        return new Domain.Models.Inventory
        {
            Id = Id,
            Name = Name,
            Customer = customer
        };
    }

    internal static List<Domain.Models.Inventory>? ConvertToDomainClass(List<Inventory> inventories)
    {
        var customerId = inventories.First().CustomerId;

        var customer = new SqLiteService().GetCustomer(customerId);

        if (customer == null) return null;
        
        var inventoryList = new List<Domain.Models.Inventory>();
        
        inventories.ForEach(inventory =>
        {
            inventoryList.Add(new Domain.Models.Inventory
            {
                Id = inventory.Id,
                Name = inventory.Name,
                Customer = customer
            });
        });

        return inventoryList;
    }
}