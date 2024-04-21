using Domain;
using Domain.Models;
using Interfaces;

namespace DAL_Account_Sqlite.Services;

public class AccountSqLiteService : IAccountDataAccessLayer
{
	public Result CreateAccount(Domain.Models.Account account) => Query.Insert(account);

	public Result<Account?> ReadAccount(string username, string password)
	{
		var account = Query.ReadFirst<Account>(username, password);

		return account is not null ? Result.FromSuccess<Account?>(account) : Result.FromError<Account?>(ErrorType.NotFound, "Username or password is incorrect", "");
	}

	public Result UpdateAccount(Account account) => Query.Update(account);

	public Result DeleteAccount(Account account) => Query.Delete(account);
}