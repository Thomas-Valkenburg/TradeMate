using DAL_Sqlite.Services;
using Dapper.Contrib.Extensions;

namespace DAL_Sqlite.Data_Access_Models;

[Table("Customer")]
internal class Customer : DbAccessModel
{
    [Key]
    public required int Id { get; init; }

    public required string Name { get; init; }

    public required string Email { get; init; }

    internal Domain.Models.Customer ConvertToDomain()
    {
        return new Domain.Models.Customer
        {
            Id       = Id,
            Name     = Name,
            Email    = Email,
            Inventory = new SqLiteService().GetAllInventories(Id)
        };
    }

    internal static Customer ConvertToDataAccess(Domain.Models.Customer customer)
    {
        return new Customer
        {
            Id    = customer.Id,
            Name  = customer.Name,
            Email = customer.Email
        };
    }

    public void Dispose()
    {
        throw new NotImplementedException();
    }
}