<<<<<<<< HEAD:DAL EF Core/Database.cs
﻿using DAL_EF_Core.Context;

namespace DAL_EF_Core;
========
﻿using TradeMate_Business.Context;

namespace TradeMate_Business;
>>>>>>>> 18a6114e3b9a14f2fd25ae9bde0ca0f20161e8ef:TradeMate Business/Database.cs

internal static class Database
{
    private static DatabaseContext? Context { get; set; }

    internal static DatabaseContext GetContext()
    {
        Context ??= new DatabaseContext();
        
        return Context;
    }

    internal static async Task SaveContext(DatabaseContext dbSet)
    {
        Context = dbSet;
        await Context.SaveChangesAsync();
    }
}