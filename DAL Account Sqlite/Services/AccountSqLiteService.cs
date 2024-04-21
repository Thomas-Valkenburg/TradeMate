using Domain;
using Domain.Models;
using Interfaces;

namespace DAL_Account_Sqlite.Services;

public class AccountSqLiteService : IAccountDataAccessLayer
{
	public Result CreateAccount(Domain.Models.Account account) => Query.Insert(account);

	public Account? ReadAccount(int id) => Query.ReadFirst<Account>(id);

	public Result UpdateAccount(Account account) => Query.Update(account);

	public Result DeleteAccount(Account account) => Query.Delete(account);
}