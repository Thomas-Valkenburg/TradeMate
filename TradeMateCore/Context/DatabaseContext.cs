using TradeMateCore.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace TradeMateCore.Context;

public class DatabaseContext : DbContext
{
    public DatabaseContext() : base("DatabaseContext")
    {
        
    }
    
    public DbSet<Inventory> Inventories { get; set; }
    public DbSet<StockItem> StockItems  { get; set; }
    public DbSet<Category>  Categories  { get; set; }

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
        modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
    }
}