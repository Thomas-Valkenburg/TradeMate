namespace Domain.Models;

public class Inventory
{
    public int Id { get; init; }

    public required string Name { get; set; }

    public required Customer Customer { get; init; }

    public List<StockItem> StockItems { get; init; } = [];

    public List<Category> Categories { get; init; } = [];
}