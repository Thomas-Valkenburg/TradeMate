using DAL_Factory;
using Domain;
using Interfaces;

namespace BLL.Models;

public class Account : Domain.Models.Account
{
	private readonly IAccountDataAccessLayer _service = Factory.GetAccountService();
	
	public required Factory.ServiceType ServiceType { get; set; }

	public Customer? Customer => Customer.TryGetCustomer(CustomerId, ServiceType).Value;

	private Result Save()
	{
		if (_service.CreateAccount(this).Error is ErrorType.Duplicate) return _service.UpdateAccount(this);
		
		return Result.FromSuccess();
	}

	public Account CreateAccount(string username, string password, string email)
	{
		var customer = new Customer(ServiceType);
		var customerResult = customer.Save();

		if (!customerResult.Success)
		{
			return null;
		}

		var account = new Account
		{
			Username     = username,
			Password     = password,
			Email        = email,
			CreationDate = DateTime.UtcNow,
			CustomerId   = customer.Id,
			ServiceType  = Factory.ServiceType.Sqlite
		};

		var accountResult = account.Save();

		return account;
	}
}