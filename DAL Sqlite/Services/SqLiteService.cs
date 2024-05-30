using DAL_Sqlite.Data_Access_Models;
using Domain;
using Interfaces;

namespace DAL_Sqlite.Services;

public class SqLiteService : IDataAccessLayer
{
    #region Customer

    public Result CreateCustomer(Domain.Models.Customer customer) => Query
        .Insert(Customer.ConvertToDataAccess(customer));
    
    public Result<Domain.Models.Customer?> GetCustomer(int customerId)
    {
        var customer = Query.ReadFirst<Customer>($"SELECT * FROM Customers WHERE Id={customerId}");

        return Result.FromSuccess(customer?.ConvertToDomain());
    }

    public Result UpdateCustomer(Domain.Models.Customer customer)
    {
        throw new NotImplementedException();
    }

    public Result DeleteCustomer(Domain.Models.Customer customer)
    {
		return Query.Delete(customer);
    }

    #endregion

    #region Inventory

    public Result CreateInventory(Domain.Models.Inventory inventory)
    {
	    var inv = new Inventory
	    {
            Id = inventory.Id,
            Name = inventory.Name,
            CustomerId = inventory.Customer.Id
	    };

	    return Query.Insert(inv);
    }
    
    public List<Domain.Models.Inventory> GetAllInventories(int customerId)
    {
	    var inventoryList = Query.ReadMany<Inventory>($"SELECT * FROM Inventories WHERE CustomerId={customerId}");

        var domainInventoriesList = new List<Domain.Models.Inventory>();
        
        inventoryList?.ForEach(inventory => domainInventoriesList.Add(inventory.ConvertToDomainClass()!));
        
        return domainInventoriesList;
    }

    public Result<Domain.Models.Inventory?> GetInventory(int inventoryId)
    {
	    var inventory = Query.ReadFirst<Inventory>($"SELECT * FROM Inventories WHERE Id={inventoryId}");

        return Result.FromSuccess(inventory?.ConvertToDomainClass());
    }

    public Result UpdateInventory(Domain.Models.Inventory inventory)
    {
        throw new NotImplementedException();
    }

    public Result DeleteInventory(Domain.Models.Inventory inventoryId)
    {
        throw new NotImplementedException();
    }

    #endregion

    #region StockItem

    public Result CreateStockItem(Domain.Models.StockItem stockItem)
    {
	    return Query.Insert(StockItem.ConvertFromDomain(stockItem));
    }

    public Result<Domain.Models.StockItem?> GetStockItem(int stockItemId)
    {
	    var stockItem = Query.ReadFirst<StockItem>($"SELECT * FROM StockItems WHERE (Id={stockItemId})");

        return Result.FromSuccess(stockItem?.ConvertToDomain());
    }

    public Domain.Models.StockItem? GetStockItemByBarcode(int inventoryId, string barcode)
    {
	    var stockItem = Query.ReadFirst<StockItem>($"SELECT * from StockItems WHERE (InventoryId={inventoryId}, Barcode={barcode})");

        return stockItem?.ConvertToDomain();
    }

    public List<Domain.Models.StockItem> GetAllStockItems(int inventoryId)
    {
	    var stockItems = Query.ReadMany<StockItem>($"SELECT * FROM StockItems Where (InventoryId={inventoryId})");

	    var domainStockItemList = new List<Domain.Models.StockItem>();

	    stockItems?.ForEach(stockItem => domainStockItemList.Add(stockItem.ConvertToDomain()));

	    return domainStockItemList;
	}

    public Result UpdateStockItem(Domain.Models.StockItem stockItem)
    {
	    return Query.Update(stockItem);
	}

    public Result DeleteStockItem(Domain.Models.StockItem stockItem)
    {
	    return Query.Delete(stockItem);
    }

    #endregion

    #region Category

    public Result CreateCategory(Domain.Models.Category category)
    {
        throw new NotImplementedException();
    }

    public Domain.Models.Category? GetCategory(int categoryId)
    {
        var category = Query
            .ReadFirstAsync<Category>($"SELECT * FROM Categories WHERE Id={categoryId}").GetAwaiter()
            .GetResult();

        return category?.ConvertToDomain();
    }
    
    public List<Domain.Models.Category> GetAllCategories(int inventoryId)
    {
        var categoryList = Query
            .ReadManyAsync<Category>($"SELECT * FROM Categories WHERE InventoryId={inventoryId}")
            .GetAwaiter().GetResult();

        if (categoryList is null) return [];

        return categoryList.ConvertAll(input => new Domain.Models.Category
        {
            Id = input.Id,
            Name = input.Name,
            Inventory = GetInventory(input.InventoryId).Value
        });
    }

    public Result UpdateCategory(Domain.Models.Category category)
    {
        throw new NotImplementedException();
    }

    public Result DeleteCategory(Domain.Models.Category categoryId)
    {
        throw new NotImplementedException();
    }

    #endregion
}