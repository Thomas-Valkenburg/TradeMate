<<<<<<<< HEAD:Test/UnitTest1.cs
using DAL_EF_Core.Context;
using Domain.Models;

namespace Test;
========
using TradeMate_Business.Context;
using TradeMate_Business.Models;

namespace TradeMate_Test;
>>>>>>>> 18a6114e3b9a14f2fd25ae9bde0ca0f20161e8ef:TradeMate Test/UnitTest1.cs

public class Tests
{
    private DatabaseContext Context { get; set; } = null!;
    
    [OneTimeSetUp]
    public void Setup()
    {
        Context = new DatabaseContext();
    }

    [Test]
    public void TestAddInventory()
    {
        var inventory = new Inventory
        {
            Name = "database1",
            Customer = new Customer
            {
                Name = "a", Email = "test@gmail.com"
            }
        };

        Context.Inventories.Add(inventory);
        
        Context.SaveChanges();

        Console.WriteLine(Context.Inventories.Count());
    }

    [OneTimeTearDown]
    public void Teardown()
    {
        Context.Dispose();
    }
}