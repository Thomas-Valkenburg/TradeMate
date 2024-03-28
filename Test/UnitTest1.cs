using DAL_EF_Core.Context;
using Domain.Models;

namespace Test;

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