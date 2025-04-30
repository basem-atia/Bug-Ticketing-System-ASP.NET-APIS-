namespace BugTicketingSystem.DAL;

public interface IRoleRepository
{
    Task<IEnumerable<Role>> GetAllRolesAsync();
    Task<Role?> GetRoleAsync(Guid id);
    void AddRoleAsync(Role role);
    void UpdateRoleAsync(Role role);
    void DeleteRoleAsync(Role role);
    Task<Role?> GetFullRoleAsync(string role);
}
