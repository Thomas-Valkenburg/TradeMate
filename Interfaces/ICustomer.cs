using Domain.Models;

namespace Interfaces;

public interface ICustomer
{
    Customer GetCustomer(int customerId);
}