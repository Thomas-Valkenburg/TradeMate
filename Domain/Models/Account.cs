using Dapper.Contrib.Extensions;

namespace Domain.Models;

public class Account
{
	[Computed]
	public int AccountId { get; init; }

	[ExplicitKey]
	public required string Username { get; set; }

	public required string Password { get; set; }
	
	public required string Email { get; set; }
	
	public required DateTime CreationDate { get; init; }
	
	public required int CustomerId { get; init; }
}