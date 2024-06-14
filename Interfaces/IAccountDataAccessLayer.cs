using Domain;
using Domain.Models;

namespace Interfaces;

public interface IAccountDataAccessLayer
{
	public Result CreateAccount(Account account);
	
	public Result<Account?> ReadAccount(string username);

	public Result UpdateAccount(Account account);
	
	public Result DeleteAccount(Account account);
}