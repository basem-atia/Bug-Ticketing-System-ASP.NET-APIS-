namespace BugTicketingSystem.DAL;

public interface IUserRepository
{
    Task<IEnumerable<User>> GetAllUsersAsync();
    Task<User?> GetUserByEmailAsync(string emailAddress);
    Task<User?> GetUserByIdAsync(Guid id);
    void AddUserAsync(User user);
    void UpdateUserAsync(User user);
    void DeleteUserAsync(User user);
}
