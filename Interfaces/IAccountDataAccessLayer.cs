using Domain;
using Domain.Models;

namespace Interfaces;

public interface IAccountDataAccessLayer
{
	public Result CreateAccount(Account account);
	
	public Account? ReadAccount(int id);

	public Result UpdateAccount(Account account);
	
	public Result DeleteAccount(Account account);
}