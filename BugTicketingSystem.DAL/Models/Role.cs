namespace BugTicketingSystem.DAL;

public class Role
{
    public Guid Id { get; set; }
    public string RoleName { get; set; } = RoleEnum.Tester.ToString();
    public ICollection<User> Users { get; set; } = new HashSet<User>();
}
