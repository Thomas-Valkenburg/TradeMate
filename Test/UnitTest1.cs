using BLL.Models;
using DAL_Factory;
using Domain;

namespace Test;

public class Tests
{
    private Customer  _validCustomer   = null!;
    private Customer  _invalidCustomer = null!;
    private Inventory _inventory       = null!;
    
    [OneTimeSetUp]
    public void Setup()
    {
        _validCustomer = new Customer(Factory.ServiceType.Test)
        {
            Id = 1
        };

        _invalidCustomer = new Customer(Factory.ServiceType.Test)
        {
            Id = 2
        };

        _validCustomer.Save();
        _invalidCustomer.Save();
    }

    #region Test cases

    [Test]
    public void Test10_CreateInventory()
    {
        var result = _validCustomer.AddInventory("Inventories Eindhoven");

        Assert.That(result.Success);
        
        _inventory = _validCustomer.GetInventories().First();
    }

    [Test]
    public void Test11_CreateInventoryDuplicateFail()
    {
        var result = _validCustomer.AddInventory("Inventories Eindhoven");
        
        Assert.That(result is { Success: false, Error: ErrorType.Duplicate });
    }

    [Test]
    public void Test12_CreateInventoryEmptyFail()
    {
        var result = _validCustomer.AddInventory("");

        Assert.That(result is { Success: false, Error: ErrorType.Null });
        
        
    }

    [Test]
    public void Test13_CreateInventoryNameTooLongFail()
    {
        var result = _validCustomer.AddInventory("Inventories Eindhoven-Amsterdam 0002");

        Assert.That(result is { Success: false, Error: ErrorType.TooLong });
    }

    [Test]
    public void Test14_GetInventory()
    {
        var inventories = _validCustomer.GetInventories();

        Assert.That(inventories, Is.Not.Empty);
    }

    [Test]
    public void Test15_GetInventoryFail()
    {
        var inventories = _invalidCustomer.GetInventories();

        Assert.That(inventories, Has.Count.Zero);
    }

    [Test]
    public void Test16_ChangeInventoryName()
    {
        var result = _inventory.ChangeName("Inventories Amsterdam");
        
        Assert.That(result.Success);
    }

    [Test]
    public void Test17_ChangeInventoryNameFailEmpty()
    {
        var result = _inventory.ChangeName("");
        
        Assert.That(result is { Success: false, Error: ErrorType.Null });
    }

    [Test]
    public void Test18_ChangeInventoryNameFailTooLong()
    {
        var result = _inventory.ChangeName("Inventories Eindhoven-Amsterdam 0002");
        
        Assert.That(result is { Success: false, Error: ErrorType.TooLong });
    }

    [Test]
    public void Test30_AddProduct()
    {
        var result = _inventory.AddStockItem("Appel", "100", 200, 2.00m);
        Console.WriteLine(_inventory.GetStockItems().Count);

        Assert.That(result.Success);
        Console.WriteLine(_inventory.GetStockItems().Count);

    }

    [Test]
    public void Test31_AddProductDuplicateBarcodeFail()
    {
        Console.WriteLine(_inventory.GetStockItems().Count);

        var result = _inventory.AddStockItem("Peer", "100", 150, 2.00m);
        
        Assert.That(result is { Success: false, Error: ErrorType.Duplicate });
    }

    [Test]
    public void Test32_AddProductNameTooLongFail()
    {
        var result = _inventory.AddStockItem("De aller grootste peer die er bestaat op de hele wereld, en in onze winkel te koop is voor de beste prijs.", "200", 150, 10);

        Assert.That(result is { Success: false, Error: ErrorType.TooLong });
    }

    [Test]
    public void Test33_AddProductEmptyNameFail()
    {
        var result = _inventory.AddStockItem("", "200", 150, 10);
        
        Assert.That(result is { Success: false, Error: ErrorType.Null });
    }

    [Test]
    public void Test34_EditProductAmount()
    {
        var stockItem = _inventory.GetStockItems().Find(x => x.Barcode == "100");
        
        var result = stockItem?.ChangeAmount(250);
        
        Assert.That(result is { Success: true });
    }

    [Test]
    public void Test35_EditProductBarcode()
    {
        var stockItem = _inventory.GetStockItems().Find(x => x.Barcode == "100");

        var result = stockItem?.ChangeBarcode("150");

        Assert.That(result is { Success: true });
    }

    [Test]
    public void Test36_EditProductNameTooLongFail()
    {
        var stockItem = _validCustomer
            .GetInventories()
            .First()
            .GetStockItems()
            .Find(x => x.Barcode == "150");

        var result = stockItem?.ChangeName("De aller grootste peer die er bestaat op de hele wereld, en in onze winkel te koop is voor de beste prijs.");

        Assert.That(result is { Success: false, Error: ErrorType.TooLong });
    }

    [Test]
    public void Test37_EditProductEmptyNameFail()
    {
        var stockItem = _inventory.GetStockItems().Find(x => x.Barcode == "150");

        var result = stockItem?.ChangeName("");

        Assert.That(result is { Success: false, Error: ErrorType.Null });
    }

    [Test]
    public void Test38_RemoveProduct()
    {
        var stockItem = _inventory.GetStockItems().Find(x => x.Barcode == "150");

        var result = stockItem?.Delete();

        Assert.That(result is { Success: true });
    }

    [Test]
    public void Test39_RemoveProductFail()
    {
        var stockItem = _inventory.GetStockItems().Find(x => x.Barcode == "150");

        var result = stockItem?.Delete();

        Assert.That(result is null or { Success: false, Error: ErrorType.NotFound });
    }

    [Test]
    public void Test50_RemoveInventory()
    {
        var result = _inventory.Delete();

        Assert.That(result is { Success: true });
    }

    #endregion

    #region Additional Tests

    [Test]
    public void Test80_CheckFactory()
    {
        Assert.DoesNotThrow(() =>
        {
            foreach (var value in Enum.GetValues(typeof(Factory.ServiceType)))
            {
                Factory.GetDataService((Factory.ServiceType)value);
            }
        });
    }

    [Test]
    public void Test81_CheckFactoryFail()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() =>
        {
            Factory.GetDataService((Factory.ServiceType)(-1));
        });
    }

    #endregion

    [OneTimeTearDown]
    public void Teardown()
    {
        
    }
}