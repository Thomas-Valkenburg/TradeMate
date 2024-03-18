using TradeMateCore.Context;

namespace TradeMateCore;

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