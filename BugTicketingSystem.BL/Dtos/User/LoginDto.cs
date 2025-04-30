namespace BugTicketingSystem.BL;

public class LoginDto
{
    public required string EmailAddress { get; set; } = string.Empty;
    public required string Password { get; set; } = string.Empty;
}
