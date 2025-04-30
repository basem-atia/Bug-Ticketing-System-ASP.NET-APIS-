
using Microsoft.EntityFrameworkCore;

namespace BugTicketingSystem.DAL;

public class RoleRepository : IRoleRepository
{
    private readonly BugTicketingContext _context;

    public RoleRepository(BugTicketingContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<Role>> GetAllRolesAsync()
    {
        return await _context.Set<Role>().AsNoTracking().ToListAsync();
    }

    public async Task<Role?> GetRoleAsync(Guid id)
    {
        return await _context.Set<Role>().FindAsync(id);
    }
    public async Task<Role?> GetFullRoleAsync(string role)
    {
        return await _context.Set<Role>().FirstOrDefaultAsync(r => r.RoleName == role);
    }
    public void AddRoleAsync(Role role)
    {
        _context.Set<Role>().Add(role);
    }

    public void DeleteRoleAsync(Role role)
    {
        _context.Set<Role>().Remove(role);
    }

    public void UpdateRoleAsync(Role role)
    {
        //_context.Set<Role>().Update(role);
    }
}
