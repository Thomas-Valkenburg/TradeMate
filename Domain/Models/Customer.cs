namespace Domain.Models;

public class Customer
{
    public int Id { get; init; }

    public List<Inventory> Inventories { get; init; } = [];
}