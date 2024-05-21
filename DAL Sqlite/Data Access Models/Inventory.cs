using DAL_Sqlite.Services;
using Dapper.Contrib.Extensions;

namespace DAL_Sqlite.Data_Access_Models;

[Table("Inventories")]
public class Inventory
{
	[Key]
	public required int Id { get; init; }

    public required string Name { get; init; }

	public required int CustomerId { get; init; }

    internal Domain.Models.Inventory? ConvertToDomainClass()
    {
        var customer = new SqLiteService().GetCustomer(CustomerId);

        if (customer.Value == null) return null;

        return new Domain.Models.Inventory
        {
            Id = Id,
            Name = Name,
            Customer = customer.Value
        };
    }

    internal static List<Domain.Models.Inventory>? ConvertToDomainClass(List<Inventory> inventories)
    {
        var customerId = inventories.First().CustomerId;

        var customer = new SqLiteService().GetCustomer(customerId);

        if (customer.Value == null) return null;

        var inventoryList = new List<Domain.Models.Inventory>();

        inventories.ForEach(inventory =>
        {
            inventoryList.Add(new Domain.Models.Inventory
            {
                Id = inventory.Id,
                Name = inventory.Name,
                Customer = customer.Value
            });
        });

        return inventoryList;
    }
}