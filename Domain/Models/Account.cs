namespace Domain.Models;

public class Account
{
	public int Id { get; set; }
	
	public required string Username { get; set; }
	
	public required string Password { get; set; }
	
	public required string Email { get; set; }
	
	public required DateTime CreationDate { get; set; }
	
	public required int CustomerId { get; set; }
}