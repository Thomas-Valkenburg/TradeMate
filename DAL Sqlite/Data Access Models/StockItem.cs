using DAL_Sqlite.Services;
using Dapper.Contrib.Extensions;

namespace DAL_Sqlite.Data_Access_Models;

[Table("StockItems")]
internal class StockItem
{
    [Key]
    public required int Id { get; init; }
    
    public required string Name { get; init; }
    
    public required string Barcode { get; init; }
    
    public required int Amount { get; init; }
    
    public required long Price { get; init; }
    
    public required int InventoryId { get; init; }

    internal Domain.Models.StockItem ConvertToDomain()
    {
        return new Domain.Models.StockItem(Barcode, Name, Amount, (decimal) Price / 100, new SqLiteService().GetInventory(InventoryId).Value)
        {
            Id = Id
        };
    }

    internal static StockItem ConvertFromDomain(Domain.Models.StockItem stockItem)
    {
	    try
	    {
		    return new StockItem
		    {
			    Id          = stockItem.Id,
			    Name        = stockItem.Name,
			    Barcode     = stockItem.Barcode,
			    Amount      = stockItem.Amount,
			    Price       = (int) (stockItem.Price * 100),
			    InventoryId = stockItem.Inventory!.Id
		    };
	    }
	    catch (OverflowException)
	    {
		    return new StockItem
		    {
			    Id          = stockItem.Id,
			    Name        = stockItem.Name,
			    Barcode     = stockItem.Barcode,
			    Amount      = stockItem.Amount,
			    Price       = long.MaxValue,
			    InventoryId = stockItem.Inventory!.Id
		    };
		}
    }
}