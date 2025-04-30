
using Microsoft.EntityFrameworkCore;

namespace BugTicketingSystem.DAL;

public class ProjectRepository : IProjectRepository
{
    private readonly BugTicketingContext _context;

    public ProjectRepository(BugTicketingContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Project>> GetAllProjectsAsync()
    {
        return await _context.Set<Project>().AsNoTracking().ToListAsync();
    }

    public async Task<Project?> GetProjectAsync(Guid id)
    {
        return await _context.Set<Project>().Include(p => p.Bugs).FirstOrDefaultAsync(p => p.Id == id);
    }
    public void AddProjectAsync(Project project)
    {
        _context.Set<Project>().Add(project);
    }

    public void DeleteProjectAsync(Project project)
    {
        _context.Set<Project>().Remove(project);
    }


    public void UpdateProjectAsync(Project project)
    {
        //_context.Set<Project>().Update(project);
    }

    public async Task<Project?> GetByName(string name)
    {
        return await _context.Set<Project>().Include(p => p.Bugs).FirstOrDefaultAsync(p => p.Name == name);
    }

    public async Task<IEnumerable<Project>> GetAllProjectsAsyncWithBugs()
    {
        return await _context.Set<Project>().Include(p => p.Bugs).AsNoTracking().ToListAsync();

    }
}
