using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL_EF_Core.Context;

public class DatabaseContext : DbContext
{
    public DatabaseContext()
    {
        Database.EnsureCreated();
    }

    public virtual DbSet<Customer>  Customers   { get; set; }
    public virtual DbSet<Inventory> Inventories { get; set; }
    public virtual DbSet<StockItem> StockItems  { get; set; }
    public virtual DbSet<Category>  Categories  { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (optionsBuilder.IsConfigured) return;

        optionsBuilder.UseSqlServer("Server=tcp:504234.database.windows.net,1433;Initial Catalog=TradeMate;Persist Security Info=False;User ID=CloudSA75d3553e;Password=Database123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;",
            options =>
            {
                options.EnableRetryOnFailure(2);
            });
    }
}