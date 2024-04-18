using DAL_Test.Data;
using Domain.Models;
using Interfaces;

namespace DAL_Test.Services;

public class TestService : IDal
{
    private readonly TempData _tempData = new();
    
    public Result CreateCustomer(Customer customer)
    {
        _tempData.Customer.Add(customer);
        
        return Result.FromSuccess();
    }

    public Customer? GetCustomer(int customerId)
    {
        return _tempData.Customer.Find(x => x.Id == customerId);
    }

    public Result UpdateCustomer(Customer customer)
    {
        var c = _tempData.Customer.Find(x => x.Id == customer.Id);
        
        if (c is null) return Result.FromError(ErrorType.NotFound, "Customer not found", "");
        
        _tempData.Customer.Remove(c);
        
        _tempData.Customer.Add(customer);
        
        return Result.FromSuccess();
    }

    public Result DeleteCustomer(int customerId)
    {
        _tempData.Customer.RemoveAll(x => x.Id == customerId);
        
        return Result.FromSuccess();
    }

    public Result CreateInventory(Inventory inventory)
    {
        var customer = _tempData.Customer.Find(x => x.Id == inventory.Customer.Id);
        
        customer?.Inventory.Add(inventory);
        
        return Result.FromSuccess();
    }

    public Inventory? GetInventory(int inventoryId)
    {
        Inventory? inventory = null;
        
        _tempData.Customer.ForEach(x =>
        {
            var inv = x.Inventory.Find(y => y.Id == inventoryId);

            if (inventory is not null) inventory = inv;
        });

        return inventory;
    }

    public List<Inventory> GetAllInventories(int customerId)
    {
        return _tempData.Customer.Find(x => x.Id == customerId)?.Inventory ?? [];
    }

    public Result UpdateInventory(Inventory inventory)
    {
        return Result.FromSuccess();
    }

    public Result DeleteInventory(int inventoryId)
    {
        _tempData.Customer.ForEach(x => x.Inventory.RemoveAll(y => y.Id == inventoryId));
        
        return Result.FromSuccess();
    }

    public Result CreateStockItem(StockItem stockItem)
    {
        var inv = stockItem.Inventory;
        
        inv?.StockItems.Add(stockItem);
        
        return Result.FromSuccess();
    }

    public StockItem? GetStockItem(int stockItemId)
    {
        StockItem? stockItem = null;
        
        _tempData.Customer.ForEach(x =>
        {
            x.Inventory.ForEach(y =>
            {
                y.StockItems.ForEach(z =>
                {
                    if (z.Id == stockItemId) stockItem = z;
                });
            });
        });
        
        return stockItem;
    }

    public StockItem? GetStockItemByBarcode(int inventoryId, int barcode)
    {
        StockItem? stockItem = null;

        _tempData.Customer.ForEach(x =>
        {
            if (x.Inventory.Any(y => y.Id == inventoryId)) return;

            var inventory = x.Inventory.Find(y => y.Id == inventoryId);

            stockItem = inventory?.StockItems.Find(y => y.Barcode == barcode);
        });

        return stockItem;
    }

    public List<StockItem> GetAllStockItems(int inventoryId)
    {
        var stockItems = new List<StockItem>();
        
        _tempData.Customer.ForEach(x =>
        {
            x.Inventory.ForEach(y =>
            {
                if (y.Id == inventoryId) stockItems.AddRange(y.StockItems);
            });
        });

        return stockItems;
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