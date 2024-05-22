using DAL_Test.Data;
using Domain;
using Domain.Models;
using Interfaces;

namespace DAL_Test.Services;

public class TestService : IDataAccessLayer
{
    private readonly TempData _tempData = new();
    
    public Result CreateCustomer(Customer customer)
    {
        _tempData.Customer.Add(customer);
        
        return Result.FromSuccess();
    }

    public Result<Customer?> GetCustomer(int customerId)
    {
        return Result.FromSuccess(_tempData.Customer.Find(x => x.Id == customerId));
    }

    public Result UpdateCustomer(Customer customer)
    {
        var c = _tempData.Customer.Find(x => x.Id == customer.Id);
        
        if (c is null) return Result.FromError(ErrorType.NotFound, "Customer not found", "");
        
        _tempData.Customer.Remove(c);
        
        _tempData.Customer.Add(customer);
        
        return Result.FromSuccess();
    }

    public Result DeleteCustomer(Customer customer)
    {
	    _tempData.Customer.RemoveAll(x => x.Id == customer.Id);

	    return Result.FromSuccess();
	}

    public Result CreateInventory(Inventory inventory)
    {
        var customer = _tempData.Customer.Find(x => x.Id == inventory.Customer.Id);
        
        customer?.Inventories.Add(inventory);
        
        return Result.FromSuccess();
    }

    public Result<Inventory?> GetInventory(int inventoryId)
    {
        Inventory? inventory = null;
        
        _tempData.Customer.ForEach(x =>
        {
            var inv = x.Inventories.Find(y => y.Id == inventoryId);

            if (inventory is not null) inventory = inv;
        });

        return Result.FromSuccess(inventory);
    }

    public List<Inventory> GetAllInventories(int customerId)
    {
        return _tempData.Customer.Find(x => x.Id == customerId)?.Inventories ?? [];
    }

    public Result UpdateInventory(Inventory inventory)
    {
        return Result.FromSuccess();
    }

    public Result DeleteInventory(Inventory inventory)
    {
        _tempData.Customer.ForEach(x => x.Inventories.RemoveAll(y => y.Id == inventory.Id));
        
        return Result.FromSuccess();
    }

    public Result CreateStockItem(StockItem stockItem)
    {
        var inv = stockItem.Inventory;
        
        if (inv is null) return Result.FromError(ErrorType.NotFound, "Inventories not found", "StockItem.Inventories");
        
        if (inv.StockItems.Any(x => x.Barcode == stockItem.Barcode)) return Result.FromError(ErrorType.Duplicate, $"There already exists a StockItem with barcode: {stockItem.Barcode}", "StockItem.Barcode");
        
        inv.StockItems.Add(stockItem);
        
        return Result.FromSuccess();
    }

    public Result<StockItem?> GetStockItem(int stockItemId)
    {
        StockItem? stockItem = null;
        
        _tempData.Customer.ForEach(x =>
        {
            x.Inventories.ForEach(y =>
            {
                y.StockItems.ForEach(z =>
                {
                    if (z.Id == stockItemId) stockItem = z;
                });
            });
        });
        
        return Result.FromSuccess(stockItem);
    }

    public StockItem? GetStockItemByBarcode(int inventoryId, string barcode)
    {
        StockItem? stockItem = null;

        _tempData.Customer.ForEach(x =>
        {
            if (x.Inventories.Any(y => y.Id == inventoryId)) return;

            var inventory = x.Inventories.Find(y => y.Id == inventoryId);

            stockItem = inventory?.StockItems.Find(y => y.Barcode == barcode);
        });

        return stockItem;
    }

    public List<StockItem> GetAllStockItems(int inventoryId)
    {
        var stockItems = new List<StockItem>();
        
        _tempData.Customer.ForEach(x =>
        {
            x.Inventories.ForEach(y =>
            {
                if (y.Id == inventoryId) stockItems.AddRange(y.StockItems);
            });
        });

        return stockItems;
    }

    public Result UpdateStockItem(StockItem stockItem)
    {
        foreach (var x in _tempData.Customer)
        {
            foreach (var y in x.Inventories)
            {
                foreach (var z in y.StockItems.Where(z => z.Id == stockItem.Id))
                {
                    y.StockItems.Remove(z);
                    y.StockItems.Add(stockItem);

                    return Result.FromSuccess();
                }
            }
        }
        
        return Result.FromError(ErrorType.NotFound, $"No stockItem found with id: {stockItem.Id}", "StockItem");
    }

    public Result DeleteStockItem(StockItem stockItem)
    {
        foreach (var x in _tempData.Customer)
        {
            foreach (var y in x.Inventories)
            {
                foreach (var z in y.StockItems.Where(z => z.Id == stockItem.Id))
                {
                    y.StockItems.Remove(z);
                    return Result.FromSuccess();
                }
            }
        }

        return Result.FromError(ErrorType.NotFound, "", "");
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

    public Result DeleteCategory(Category categoryId)
    {
	    throw new NotImplementedException();
    }

    public Result DeleteCategory(int categoryId)
    {
        return Result.FromSuccess();
    }
}