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
		return _service.CreateAccount(this).Error is ErrorType.Duplicate ? _service.UpdateAccount(this) : Result.FromSuccess();
	}

	public static Result CreateAccount(string username, string password, string email)
	{
		var customer = new Customer(Factory.ServiceType.Sqlite);
		var customerResult = customer.Save();

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

		if (!accountResult.Success) return accountResult;
		if (!customerResult.Success) return customerResult;
		
		return Result.FromSuccess();
	}

	public static Result<Account?> TryLogin(string username, string password)
	{
		var result = Factory.GetAccountService().ReadAccount(username, password);

		if (!result.Success || result.Value is null) return Result.FromError<Account>((ErrorType)result.Error!, result.ErrorMessage, result.ErrorPropertyName);

		return Result.FromSuccess(Convert(result.Value, Factory.ServiceType.Sqlite))!;
	}

	private static Account Convert(Domain.Models.Account account, Factory.ServiceType serviceType)
	{
		return new Account
		{
			Username     = account.Username,
			Password     = account.Password,
			Email        = account.Email,
			CustomerId   = account.CustomerId,
			CreationDate = account.CreationDate,
			ServiceType  = serviceType,
			Id           = account.Id
		};
	}
}