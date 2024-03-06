using System.ComponentModel.DataAnnotations;

namespace TradeMateCore.Models;

public class StockItem(string name, IEnumerable<Category>? categories)
{
    [Key]
    public int Id { get; set; }

    [StringLength(255)]
    public string Name { get; set; } = name;
    
    public IEnumerable<Category> Categories { get; } = categories ?? [];
}