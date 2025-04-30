namespace BugTicketingSystem.BL;

public class RegisterDto
{
    public required string FullName { get; set; } = string.Empty;
    public required string EmailAddress { get; set; } = string.Empty;
    public required string Password { get; set; } = string.Empty;
}
