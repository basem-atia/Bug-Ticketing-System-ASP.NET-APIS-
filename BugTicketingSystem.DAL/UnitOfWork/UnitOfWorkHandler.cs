using BugTicketingSystem.DAL.Repos.BugRepo;

namespace BugTicketingSystem.DAL
{
    public class UnitOfWorkHandler : IUnitOfWork
    {
        private readonly BugTicketingContext _context = null!;
        public IBugRepository BugRepository { get; }

        public IFileAttachmentRepository FileAttachmentRepository { get; }

        public IProjectRepository ProjectRepository { get; }

        public IRoleRepository RoleRepository { get; }

        public IUserRepository UserRepository { get; }

        public UnitOfWorkHandler(
            BugTicketingContext context,
            IBugRepository bugRepository,
            IFileAttachmentRepository fileAttachmentRepository,
            IProjectRepository projectRepository,
            IRoleRepository roleRepository,
            IUserRepository userRepository)
        {
            _context = context;
            BugRepository = bugRepository;
            FileAttachmentRepository = fileAttachmentRepository;
            ProjectRepository = projectRepository;
            RoleRepository = roleRepository;
            UserRepository = userRepository;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
