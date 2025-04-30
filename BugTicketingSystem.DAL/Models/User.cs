namespace BugTicketingSystem.DAL
{
    public class User
    {
        public Guid Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string EmailAddress { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public ICollection<Bug> Bugs { get; set; } = new HashSet<Bug>();
        public ICollection<Role> Roles { get; set; } = new HashSet<Role>();

    }
}
