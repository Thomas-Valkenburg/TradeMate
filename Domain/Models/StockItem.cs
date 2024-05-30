namespace Domain.Models;

public class StockItem
{
    public StockItem()
    {
    }

	public StockItem(string barcode, string name, int amount, decimal price, Inventory inventory)
	{
		Barcode = barcode;
		Name = name;
        Amount = amount;
        Price = price;
        Inventory = inventory;
	}

    public int Id { get; init; }

    public string Barcode { get; protected set; }

    public string Name { get; protected set; }
    
    public int Amount { get; protected set; }
    
    public decimal Price { get; protected set; }

    public Inventory? Inventory { get; init; }

    public List<Category> Categories { get; init; } = [];
}