using Domain;
using Interfaces;

namespace DAL_Sqlite.Services;

public class SqLiteService : IDataAccessLayer
{
    #region Customer

    public Result CreateCustomer(Domain.Models.Customer customer) => Query
        .Insert(Data_Access_Models.Customer.ConvertToDataAccess(customer));
    
    public Result<Domain.Models.Customer?> GetCustomer(int customerId)
    {
        var customer = Query.ReadFirst<Data_Access_Models.Customer>($"SELECT * FROM Customer WHERE Id={customerId}");

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
        throw new NotImplementedException();
    }
    
    public List<Domain.Models.Inventory> GetAllInventories(int customerId)
    {
	    var inventoryList = Query.ReadMany<Data_Access_Models.Inventory>($"SELECT * FROM Inventory WHERE CustomerId={customerId}");

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
        throw new NotImplementedException();
    }

    public Domain.Models.StockItem? GetStockItem(int stockItemId)
    {
	    var stockItem = Query.ReadFirst<Data_Access_Models.StockItem>($"SELECT * FROM StockItems WHERE Id={stockItemId}");

        return stockItem?.ConvertToDomain();
    }

    public Domain.Models.StockItem? GetStockItemByBarcode(int inventoryId, string barcode)
    {
        throw new NotImplementedException();
    }

    public List<Domain.Models.StockItem> GetAllStockItems(int inventoryId)
    {
	    var stockItems = Query.ReadMany<Data_Access_Models.StockItem>($"SELECT * FROM StockItems Where InventoryId={inventoryId}");

	    var domainStockItemList = new List<Domain.Models.StockItem>();

	    stockItems?.ForEach(stockItem => domainStockItemList.Add(stockItem.ConvertToDomain()!));

	    return domainStockItemList;
	}

    public Result UpdateStockItem(Domain.Models.StockItem stockItem)
    {
        throw new NotImplementedException();
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
            .ReadFirstAsync<Data_Access_Models.Category>($"SELECT * FROM Category WHERE Id={categoryId}").GetAwaiter()
            .GetResult();

        return category?.ConvertToDomain();
    }
    
    public List<Domain.Models.Category> GetAllCategories(int inventoryId)
    {
        var categoryList = Query
            .ReadManyAsync<Data_Access_Models.Category>($"SELECT * FROM Category WHERE InventoryId={inventoryId}")
            .GetAwaiter().GetResult();

        if (categoryList is null) return [];

        return categoryList.ConvertAll(input => new Domain.Models.Category
        {
            Id = input.Id,
            Name = input.Name,
            Inventory = GetInventory(input.InventoryId)
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