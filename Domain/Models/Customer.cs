using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models;

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