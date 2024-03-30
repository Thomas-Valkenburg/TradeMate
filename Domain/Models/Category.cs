namespace Domain.Models;

public class Category
{
    public int Id { get; init; }

    public required string Name { get; set; }

    public required Inventory Inventory { get; init; }

    public List<StockItem> StockItems { get; init; } = [];
}