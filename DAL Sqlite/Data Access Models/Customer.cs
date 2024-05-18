using DAL_Sqlite.Services;
using Dapper.Contrib.Extensions;

namespace DAL_Sqlite.Data_Access_Models;

[Table("Customer")]
internal class Customer
{
    [Key]
    public required int Id { get; init; }

    internal Domain.Models.Customer ConvertToDomain()
    {
	    return new Domain.Models.Customer
	    {
		    Id = Id
	    };
    }

    internal static Customer ConvertToDataAccess(Domain.Models.Customer customer)
    {
	    return new Customer
	    {
		    Id = customer.Id
	    };
    }
}