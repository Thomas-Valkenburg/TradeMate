using BLL.Models;

namespace Test;

public class Tests
{
    [OneTimeSetUp]
    public void Setup()
    {
        
    }

    [Test]
    public void TestCase01_AddInventory()
    {
        Inventory.GetAllInventories(1);
    }

    [OneTimeTearDown]
    public void Teardown()
    {
        
    }
}