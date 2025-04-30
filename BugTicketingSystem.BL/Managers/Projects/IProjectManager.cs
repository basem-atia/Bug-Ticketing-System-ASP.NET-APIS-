namespace BugTicketingSystem.BL;

public interface IProjectManager
{
    Task<GeneralResult> AddProject(ProjectAddDto project);
    Task<GeneralResult> GetAllProject();
    Task<GeneralResult> GetById(Guid id);
    Task<GeneralResult> GetAllProjectsWithBugs();


}
