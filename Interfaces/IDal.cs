using Domain.Models;

namespace Interfaces;

public interface IDal
{
    #region Customer

    Result CreateCustomer(Customer customer);

    Customer? GetCustomer(int customerId);

    Result UpdateCustomer(Customer customer);

    Result DeleteCustomer(int customerId);

    #endregion

    #region Inventory

    Result CreateInventory(Inventory inventory);

    Inventory? GetInventory(int inventoryId);
    
    List<Inventory> GetAllInventories(int customerId);
    
    Result UpdateInventory(Inventory inventory);
    
    Result DeleteInventory(int inventoryId);

    #endregion

    #region StockItem

    Result CreateStockItem(StockItem stockItem);
    
    StockItem? GetStockItem(int stockItemId);

    StockItem? GetStockItemByBarcode(int inventoryId, int barcode);
    
    List<StockItem> GetAllStockItems(int inventoryId);
    
    Result UpdateStockItem(StockItem stockItem);
    
    Result DeleteStockItem(int stockItemId);

    #endregion

    #region Category

    Result CreateCategory(Category category);
    
    Category? GetCategory(int categoryId);
    
    List<Category> GetAllCategories(int inventoryId);
    
    Result UpdateCategory(Category category);
    
    Result DeleteCategory(int categoryId);

    #endregion
}