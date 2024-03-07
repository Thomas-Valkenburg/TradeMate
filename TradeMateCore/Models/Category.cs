using System.ComponentModel.DataAnnotations;

namespace TradeMateCore.Models;

public class Category(string name)
{
    public int Id { get; }

    public string Name { get; set; } = name;
}