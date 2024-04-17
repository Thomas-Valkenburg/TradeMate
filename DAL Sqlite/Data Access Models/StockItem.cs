using DAL_Sqlite.Services;
using Dapper.Contrib.Extensions;

namespace DAL_Sqlite.Data_Access_Models;

[Table("StockItem")]
internal class StockItem : DbAccessModel
{
    [Key]
    public required int Id { get; init; }
    
    public required string Name { get; init; }
    
    public required int Barcode { get; init; }
    
    public required int Amount { get; init; }
    
    public required decimal Price { get; init; }
    
    public required int InventoryId { get; init; }

    internal Domain.Models.StockItem ConvertToDomain()
    {
        return new Domain.Models.StockItem
        {
            Id = Id,
            Name = Name,
            Barcode = Barcode,
            Amount = Amount,
            Price = Price,
            Inventory = new SqLiteService().GetInventory(InventoryId)
        };
    }
}