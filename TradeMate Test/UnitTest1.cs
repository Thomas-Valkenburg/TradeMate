using TradeMate_Business.Context;
using TradeMate_Business.Models;

namespace TradeMate_Test;

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
        var inventory = new Inventory("database1", new Customer {Name = "a", Email = "test@gmail.com"});

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