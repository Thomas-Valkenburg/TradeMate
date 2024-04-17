namespace Domain.Models;

public class StockItem
{
    public int Id { get; init; }

    public required int Barcode { get; set; }

    public required string Name { get; set; }
    
    public required int Amount { get; set; }
    
    public required decimal Price { get; set; }

    public required Inventory? Inventory { get; init; }

    public List<Category> Categories { get; init; } = [];
}