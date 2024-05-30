using DAL_Sqlite.Services;
using Dapper.Contrib.Extensions;

namespace DAL_Sqlite.Data_Access_Models;

[Table("Categories")]
internal class Category
{
    [Key]
    public required int Id { get; init; }
        
    public required string Name { get; init; }

    public required int InventoryId { get; init; }

    internal Domain.Models.Category ConvertToDomain()
    {
        return new Domain.Models.Category(Name, new SqLiteService().GetInventory(InventoryId).Value)
        {
            Id = Id
        };
    }
}