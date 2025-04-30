namespace BugTicketingSystem.DAL;

public interface IBugRepository
{
    Task<IEnumerable<Bug>> GetAllBugsAsync();
    Task<Bug?> GetBugAsync(Guid id);
    void AddBugAsync(Bug bug);
    void UpdateBugAsync(Bug bug);
    void DeleteBugAsync(Bug bug);
}
