using TradeMateCore.Context;
using TradeMateCore.Models;

namespace TradeMateTest;

public class Tests
{
    private DatabaseContext Context { get; set; } = null!;
    
    [OneTimeSetUp]
    public void Setup()
    {
        Context = new DatabaseContext();
        //Context.Database.Delete();
        if (Context.Database.CreateIfNotExists()) Context.SaveChanges();
    }

    [Test]
    public void TestAddInventory()
    {
        var inventory = new Inventory("database1");
        inventory.AddStockItem(new StockItem(1000, "Appel", []));

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