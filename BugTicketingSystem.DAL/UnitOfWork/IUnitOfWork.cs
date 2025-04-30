namespace BugTicketingSystem.DAL;

public interface IUnitOfWork
{
    public IBugRepository BugRepository { get; }
    public IFileAttachmentRepository FileAttachmentRepository { get; }
    public IProjectRepository ProjectRepository { get; }
    public IRoleRepository RoleRepository { get; }
    public IUserRepository UserRepository { get; }
    Task<int> SaveChangesAsync();
}
