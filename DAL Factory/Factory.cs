using DAL_Account_Sqlite.Services;
using DAL_EF_Core.Services;
using DAL_Sqlite.Services;
using DAL_Test.Services;
using Interfaces;

namespace DAL_Factory;

public static class Factory
{
	private static readonly List<IAccountDataAccessLayer> AccountServices = [];
	private static readonly List<IDataAccessLayer>        Services        = [];

	public static IAccountDataAccessLayer GetAccountService()
	{
		var service = AccountServices.Find(service => service.GetType() == typeof(AccountSqLiteService)) as AccountSqLiteService;

        if (service is not null) return service;

        var newService = new AccountSqLiteService();

        AccountServices.Add(new AccountSqLiteService());

        return newService;
	}
    
    public static IDataAccessLayer GetDataService(ServiceType type)
    {
	    IDataAccessLayer? service = type switch
	    {
		    ServiceType.Sqlite => Services.Find(service => service.GetType() == typeof(SqLiteService)) as SqLiteService,
		    ServiceType.EfCore => Services.Find(service => service.GetType() == typeof(EfCoreService)) as EfCoreService,
		    ServiceType.Test   => Services.Find(service => service.GetType() == typeof(TestService)) as TestService,
		    _                  => throw new ArgumentOutOfRangeException(nameof(type), type, null)
	    };

	    if (service is not null) return service;

	    service = type switch
        {
            ServiceType.Sqlite => new SqLiteService(),
            ServiceType.EfCore => new EfCoreService(),
            ServiceType.Test => new TestService(),
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null),
        };

        Services.Add(service);

        return service;
    }

    public enum ServiceType
    {
        Sqlite,
        EfCore,
        Test,
    }
}