<<<<<<<< HEAD:DAL EF Core/Context/DatabaseContext.cs
﻿using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL_EF_Core.Context;
========
﻿using Microsoft.EntityFrameworkCore;
using TradeMate_Business.Models;

namespace TradeMate_Business.Context;
>>>>>>>> 18a6114e3b9a14f2fd25ae9bde0ca0f20161e8ef:TradeMate Business/Context/DatabaseContext.cs

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