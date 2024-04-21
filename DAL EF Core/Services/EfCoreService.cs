using Domain;
using Domain.Models;
using Interfaces;

namespace DAL_EF_Core.Services;

public class EfCoreService : IDataAccessLayer
{
    public Result CreateCustomer(Customer customer)
    {
        throw new NotImplementedException();
    }

    public Result<Customer?> GetCustomer(int customerId)
    {
        throw new NotImplementedException();
    }

    public Result UpdateCustomer(Customer customer)
    {
        throw new NotImplementedException();
    }

    public Result DeleteCustomer(int customerId)
    {
        throw new NotImplementedException();
    }

    public Result CreateInventory(Inventory inventory)
    {
        throw new NotImplementedException();
    }

    public Inventory? GetInventory(int inventoryId)
    {
        throw new NotImplementedException();
    }

    public List<Inventory> GetAllInventories(int customerId)
    {
        throw new NotImplementedException();
    }

    public Result UpdateInventory(Inventory inventory)
    {
        throw new NotImplementedException();
    }

    public Result DeleteInventory(int inventoryId)
    {
        throw new NotImplementedException();
    }

    public Result CreateStockItem(StockItem stockItem)
    {
        throw new NotImplementedException();
    }

    public StockItem? GetStockItem(int stockItemId)
    {
        throw new NotImplementedException();
    }

    public StockItem? GetStockItemByBarcode(int inventoryId, string barcode)
    {
        throw new NotImplementedException();
    }

    public List<StockItem> GetAllStockItems(int inventoryId)
    {
        throw new NotImplementedException();
    }

    public Result UpdateStockItem(StockItem stockItem)
    {
        throw new NotImplementedException();
    }

    public Result DeleteStockItem(int stockItemId)
    {
        throw new NotImplementedException();
    }

    public Result CreateCategory(Category category)
    {
        throw new NotImplementedException();
    }

    public Category? GetCategory(int categoryId)
    {
        throw new NotImplementedException();
    }

    public List<Category> GetAllCategories(int inventoryId)
    {
        throw new NotImplementedException();
    }

    public Result UpdateCategory(Category category)
    {
        throw new NotImplementedException();
    }

    public Result DeleteCategory(int categoryId)
    {
        throw new NotImplementedException();
    }
}