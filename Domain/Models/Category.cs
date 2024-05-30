namespace Domain.Models;

public class Category
{
    public Category()
    {
    }

    public Category(string name, Inventory inventory)
    {
	    Name = name;
	    Inventory = inventory;
    }

    public int Id { get; init; }

    public string Name { get; protected set; }

    public Inventory Inventory { get; init; }

    public List<StockItem> StockItems { get; init; } = [];
}