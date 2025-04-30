
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BugTicketingSystem.DAL;

public class UserRepository : IUserRepository
{
    private readonly BugTicketingContext _context;
    private readonly IRoleRepository _repository;

    public UserRepository(BugTicketingContext context, IRoleRepository repository)
    {
        _context = context;
        _repository = repository;
    }
    public async Task<IEnumerable<User>> GetAllUsersAsync()
    {
        return await _context.Set<User>().AsNoTracking().ToListAsync();
    }

    public async Task<User?> GetUserByEmailAsync(string emailAddress)
    {
        return await _context.Set<User>().Include(u => u.Roles).Include(u => u.Bugs).FirstOrDefaultAsync(u => u.EmailAddress == emailAddress.ToLower());
    }

    public void AddUserAsync(User user)
    {
        _context.Set<User>().Add(user);
    }

    public void DeleteUserAsync(User user)
    {
        _context.Set<User>().Remove(user);
    }

    public void UpdateUserAsync(User user)
    {
        _context.Set<User>().Update(user);
    }

    public async Task<User?> GetUserByIdAsync(Guid id)
    {
        return await _context.Set<User>().Include(u => u.Roles).Include(u => u.Bugs).FirstOrDefaultAsync(u => u.Id == id);
    }
}
