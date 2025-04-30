
using Microsoft.EntityFrameworkCore;

namespace BugTicketingSystem.DAL.Repos.BugRepo;

public class BugRepository : IBugRepository
{
    private readonly BugTicketingContext _context;

    public BugRepository(BugTicketingContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<Bug>> GetAllBugsAsync()
    {
        return await _context.Set<Bug>().AsNoTracking().ToListAsync();
    }
    public async Task<Bug?> GetBugAsync(Guid id)
    {
        return await _context.Set<Bug>()
            .Include(b => b.Users)
            .Include(b => b.FileAttachments)
            .FirstOrDefaultAsync(b => b.Id == id);
    }

    public void AddBugAsync(Bug bug)
    {
        _context.Set<Bug>().Add(bug);
    }

    public void DeleteBugAsync(Bug bug)
    {
        _context.Set<Bug>().Remove(bug);
    }

    public void UpdateBugAsync(Bug bug)
    {
        //_context.Set<Bug>().Update(bug);
    }
}
