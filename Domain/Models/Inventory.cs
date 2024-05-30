namespace Domain.Models;

public class Inventory
{
    public Inventory()
	{
	}

    public Inventory(string name, Customer customer)
	{
		Name = name;
		Customer = customer;
	}

    public int Id { get; init; }

    public string Name { get; protected set; }

    public Customer Customer { get; init; }

    public List<StockItem> StockItems { get; init; } = [];

    public List<Category> Categories { get; init; } = [];
}