namespace BugTicketingSystem.DAL;

public interface IProjectRepository
{
    Task<IEnumerable<Project>> GetAllProjectsAsync();
    Task<IEnumerable<Project>> GetAllProjectsAsyncWithBugs();
    Task<Project?> GetProjectAsync(Guid id);
    Task<Project?> GetByName(string name);
    void AddProjectAsync(Project project);
    void UpdateProjectAsync(Project project);
    void DeleteProjectAsync(Project project);
}
