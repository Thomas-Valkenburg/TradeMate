
namespace DAL_Sqlite.Gateways;

public class CustomerGateway
{
    public async Task<Domain.Models.Customer> GetCustomer(int customerId)
    {
        return await Query.ExecuteFirst<Domain.Models.Customer>($"SELECT * FROM Customer WHERE Id={customerId}");
    }
}