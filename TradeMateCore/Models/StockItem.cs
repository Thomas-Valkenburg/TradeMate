using System.ComponentModel.DataAnnotations;

namespace TradeMateCore.Models;

public class StockItem(string name, IEnumerable<Category>? categories)
{
    public int Id { get; set; }

    public string Name { get; set; } = name;

    public IEnumerable<Category> Categories { get; } = categories ?? [];
}