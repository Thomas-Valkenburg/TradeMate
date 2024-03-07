using TradeMateCore.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace TradeMateCore.Context;

public class DatabaseContext : DbContext
{
    public DatabaseContext() : base("Server=tcp:504234.database.windows.net,1433; Initial Catalog=TradeMate; Persist Security Info=False; User ID=CloudSA75d3553e; Password=Database123; MultipleActiveResultSets=False; Encrypt=True; TrustServerCertificate=False; Connection Timeout=30;")
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