using Domain.Models;

namespace Interfaces;

public interface IDal
{
    #region Inventory

    List<Inventory>? GetAllInventories(int customerId);

    void CreateInventory(Inventory inventory);

    #endregion

    #region Customer

    Customer? GetCustomer(int customerId);

    void SaveCustomer(Customer customer);

    #endregion
}