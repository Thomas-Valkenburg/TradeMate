using DAL_Sqlite.Gateways;
using Domain.Models;
using Interfaces;

namespace DAL_Factory;

public class CustomerService : ICustomer
{
    public Customer GetCustomer(int customerId)
    {
        return new CustomerGateway().GetCustomer(customerId).GetAwaiter().GetResult();
    }
}