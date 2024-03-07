using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TradeMateCore.Models;

public class StockItem(int id, string name, IEnumerable<Category>? categories)
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int Id { get; set; } = id;

    [StringLength(255)]
    public string Name { get; set; } = name;

    public virtual IEnumerable<Category> Categories { get; } = categories ?? [];
}