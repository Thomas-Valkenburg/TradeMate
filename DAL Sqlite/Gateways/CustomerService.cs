using Interfaces;

namespace DAL_Sqlite.Gateways;

public class CustomerService : ICustomer
{
    public Domain.Models.Customer GetCustomer(int customerId)
    {
        return Query.ExecuteFirst<Domain.Models.Customer>($"SELECT * FROM Customer WHERE Id={customerId}").GetAwaiter().GetResult();
    }
}