using Domain.Models;
using Interfaces;

namespace DAL_Test.Services;

public class TestService : IDal
{
    public Result CreateCustomer(Customer customer)
    {
        throw new NotImplementedException();
    }

    public Customer? GetCustomer(int customerId)
    {
        throw new NotImplementedException();
    }

    public Result UpdateCustomer(Customer customer)
    {
        throw new NotImplementedException();
    }

    public Result DeleteCustomer(int customerId)
    {
        return Result.FromSuccess();
    }

    public Result CreateInventory(Inventory inventory)
    {
        return Result.FromSuccess();
    }

    public Inventory? GetInventory(int inventoryId)
    {
        return new Inventory
        {
            Id = 1,
            Name = "Test Inventory",
            Customer = new Customer
            {
                Id = 1,
                Name = "User 1",
                Email = "user@trademate.com"
            }
        };
    }

    public List<Inventory> GetAllInventories(int customerId)
    {
        if (customerId == 1)
        {
            return
            [
                new Inventory
                {
                    Id = 1,
                    Name = "Test Inventory",
                    Customer = new Customer
                    {
                        Id = 1,
                        Name = "User 1",
                        Email = "user@trademate.com"
                    }
                }
            ];
        }

        return [];
    }

    public Result UpdateInventory(Inventory inventory)
    {
        return Result.FromSuccess();
    }

    public Result DeleteInventory(int inventoryId)
    {
        return Result.FromSuccess();
    }

    public Result CreateStockItem(StockItem stockItem)
    {
        return Result.FromSuccess();
    }

    public StockItem? GetStockItem(int stockItemId)
    {
        throw new NotImplementedException();
    }

    public StockItem? GetStockItemByBarcode(int barcode)
    {
        throw new NotImplementedException();
    }

    public List<StockItem> GetAllStockItems(int inventoryId)
    {
        throw new NotImplementedException();
    }

    public Result UpdateStockItem(StockItem stockItem)
    {
        return Result.FromSuccess();
    }

    public Result DeleteStockItem(int stockItemId)
    {
        return Result.FromSuccess();
    }

    public Result CreateCategory(Category category)
    {
        return Result.FromSuccess();
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
        return Result.FromSuccess();
    }

    public Result DeleteCategory(int categoryId)
    {
        return Result.FromSuccess();
    }
}