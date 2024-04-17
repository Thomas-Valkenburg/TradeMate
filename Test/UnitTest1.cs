using BLL.Models;
using DAL_Factory;

namespace Test;

public class Tests
{
    private Customer _validCustomer   = null!;
    private Customer _invalidCustomer = null!;
    
    [OneTimeSetUp]
    public void Setup()
    {
        _validCustomer = new Customer(Factory.ServiceType.Test)
        {
            Id = 1,
            Name = "User 1",
            Email = "user1@trademate.com"
        };

        _invalidCustomer = new Customer(Factory.ServiceType.Test)
        {
            Id = 2,
            Name = "User 2",
            Email = "user2@trademate.com"
        };
    }

    #region Test cases

    [Test]
    public void Test01_GetInventory()
    {
        var inventories = _validCustomer.GetAllInventories();

        if (inventories.Count > 0) Assert.Pass("Success: Inventory Exists");
        
        Assert.Fail();
    }

    [Test]
    public void Test02_GetInventoryFail()
    {
        var inventories = _invalidCustomer.GetAllInventories();
        
        if (inventories.Count < 1) Assert.Pass("Error: Inventory doesn't exist");
        
        Assert.Fail();
    }

    [Test]
    public void Test03_CreateInventory()
    {
        var result = _validCustomer.AddInventory("Inventory Eindhoven");

        if (result.Success) Assert.Pass("Success: Added new inventory");

        Assert.Fail();
    }

    [Test]
    public void Test04_CreateInventoryDuplicateFail()
    {
        var result = _validCustomer.AddInventory("Inventory Eindhoven");
        
        if (!result.Success) Assert.Pass("Error: Failed to create");
        
        Assert.Fail();
    }

    [Test]
    public void Test05_CreateInventoryEmptyFail()
    {
        var result = _validCustomer.AddInventory("");

        if (!result.Success) Assert.Pass("Error: Failed to create");

        Assert.Fail();
    }

    [Test]
    public void Test06_CreateInventoryNameTooLongFail()
    {
        var result = _validCustomer.AddInventory("Inventory Eindhoven-Amsterdam 0002");
        
        if (!result.Success) Assert.Pass("Error: Failed to create");
    }

    [Test]
    public void Test07_AddProduct()
    {
        Assert.DoesNotThrow(() => _validCustomer.GetAllInventories().First().AddStockItem("Appel", 100, 200, 2.00m));
    }

    [Test]
    public void Test08_AddProductDuplicateBarcodeFail()
    {
        Assert.Throws<Exception>(() => _validCustomer.GetAllInventories().First().AddStockItem("Peer", 100, 150, 2.00m));
    }

    [Test]
    public void Test09_AddProductNameTooLongFail()
    {
        throw new NotImplementedException();
    }

    [Test]
    public void Test10_AddProductEmptyNameFail()
    {
        throw new NotImplementedException();
    }

    [Test]
    public void Test11_EditProductAmount()
    {
        throw new NotImplementedException();
    }

    [Test]
    public void Test12_EditProductBarcode()
    {
        throw new NotImplementedException();
    }

    [Test]
    public void Test13_EditProductNameTooLongFail()
    {
        throw new NotImplementedException();
    }

    [Test]
    public void Test14_EditProductEmptyNameFail()
    {
        throw new NotImplementedException();
    }

    [Test]
    public void Test15_RemoveProduct()
    {
        throw new NotImplementedException();
    }

    [Test]
    public void Test16_RemoveProductFail()
    {
        throw new NotImplementedException();
    }

    #endregion

    #region Additional Tests

    [Test]
    public void Test50_CheckFactory()
    {
        Assert.DoesNotThrow(() =>
        {
            foreach (var value in Enum.GetValues(typeof(Factory.ServiceType)))
            {
                Factory.GetService((Factory.ServiceType)value);
            }
        });
    }

    [Test]
    public void Test51_CheckFactoryFail()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() =>
        {
            Factory.GetService((Factory.ServiceType)(-1));
        });
    }

    #endregion

    [OneTimeTearDown]
    public void Teardown()
    {
        
    }
}