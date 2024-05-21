using Domain;
using Domain.Models;

namespace Interfaces;

public interface IDataAccessLayer
{
    #region Customer

    Result CreateCustomer(Customer customer);

    Result<Customer?> GetCustomer(int customerId);

    Result UpdateCustomer(Customer customer);

    Result DeleteCustomer(Customer customerId);

    #endregion

    #region Inventory

    Result CreateInventory(Inventory inventory);

    Result<Inventory?> GetInventory(int inventoryId);
    
    List<Inventory> GetAllInventories(int customerId);
    
    Result UpdateInventory(Inventory inventory);
    
    Result DeleteInventory(Inventory inventoryId);

    #endregion

    #region StockItem

    Result CreateStockItem(StockItem stockItem);
    
    StockItem? GetStockItem(int stockItemId);

    StockItem? GetStockItemByBarcode(int inventoryId, string barcode);
    
    List<StockItem> GetAllStockItems(int inventoryId);
    
    Result UpdateStockItem(StockItem stockItem);
    
    Result DeleteStockItem(StockItem stockItemId);

    #endregion

    #region Category

    Result CreateCategory(Category category);
    
    Category? GetCategory(int categoryId);
    
    List<Category> GetAllCategories(int inventoryId);
    
    Result UpdateCategory(Category category);
    
    Result DeleteCategory(Category categoryId);

    #endregion
}