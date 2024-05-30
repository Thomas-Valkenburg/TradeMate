using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL_EF_Core.Context;

public class DatabaseContext : DbContext
{
    public DatabaseContext()
    {
	    // ReSharper disable once VirtualMemberCallInConstructor
	    Database.EnsureCreated();
    }

    public DbSet<Customer>  Customers   { get; set; }
    public DbSet<Inventory> Inventories { get; set; }
    public DbSet<StockItem> StockItems  { get; set; }
    public DbSet<Category>  Categories  { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (optionsBuilder.IsConfigured) return;

        optionsBuilder.UseSqlServer("",
            options =>
            {
                options.EnableRetryOnFailure(2);
            });
    }
}