using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

<<<<<<<< HEAD:Domain/Models/Customer.cs
namespace Domain.Models;
========
namespace TradeMate_Business.Models;
>>>>>>>> 18a6114e3b9a14f2fd25ae9bde0ca0f20161e8ef:TradeMate Business/Models/Customer.cs

[Table(nameof(Customer))]
public class Customer
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; init; }

    [StringLength(255)]
    public required string Name { get; set; }

    [EmailAddress]
    public required string Email { get; set; }

    public virtual List<Inventory> Inventory { get; } = [];
}