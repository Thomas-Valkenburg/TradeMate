using Domain;
using Interfaces;

namespace DAL_Account_Sqlite.Services;

public class AccountSqLiteService : IAccountDataAccessLayer
{
	public Result CreateAccount(Domain.Models.Account account) => Query.Insert(account);

	public Result<Domain.Models.Account?> ReadAccount(string username)
	{
		var account = Query.ReadFirst<Domain.Models.Account>(username);

		return account is not null ? Result.FromSuccess<Domain.Models.Account?>(account) : Result.FromError<Domain.Models.Account?>(ErrorType.NotFound, "Username or password is incorrect", "");
	}

	public Result UpdateAccount(Domain.Models.Account account) => Query.Update(account);

	public Result DeleteAccount(Domain.Models.Account account) => Query.Delete(account);
}