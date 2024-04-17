using DAL_EF_Core.Services;
using DAL_Sqlite.Services;
using DAL_Test.Services;
using Interfaces;

namespace DAL_Factory;

public static class Factory
{
    public static IDal GetService(ServiceType type)
    {
        return type switch
        {
            ServiceType.Sqlite => new SqLiteService(),
            ServiceType.EfCore => new EfCoreService(),
            ServiceType.Test => new TestService(),
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null),
        };
    }

    public enum ServiceType
    {
        Sqlite,
        EfCore,
        Test
    }
}