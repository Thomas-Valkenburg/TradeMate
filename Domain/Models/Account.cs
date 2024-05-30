using Dapper.Contrib.Extensions;

namespace Domain.Models;

public class Account
{
	public Account()
	{
	}

	public Account(string username, string password, string email, int customerId)
	{
		Username = username;
		Password = password;
		Email = email;
		CreationDate = DateTime.UtcNow;
		CustomerId = customerId;
	}

	[Computed]
	public int AccountId { get; init; }

	[ExplicitKey]
	public string Username { get; init; }

	public string Password { get; protected set; }
	
	public string Email { get; protected set; }
	
	public DateTime CreationDate { get; init; }
	
	public int CustomerId { get; init; }
}