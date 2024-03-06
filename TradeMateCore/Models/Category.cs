using System.ComponentModel.DataAnnotations;

namespace TradeMateCore.Models;

public class Category(string name)
{
    [Key]
    public int Id { get; }

    [StringLength(255)]
    public string Name { get; set; } = name;
}