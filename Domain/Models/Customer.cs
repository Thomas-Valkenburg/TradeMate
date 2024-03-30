namespace Domain.Models;

public class Customer()
{
    public int Id { get; init; }

    public required string Name { get; set; }

    public required string Email { get; set; }

    public List<Inventory> Inventory { get; init; } = [];
}