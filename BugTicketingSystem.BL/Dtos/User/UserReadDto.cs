using BugTicketingSystem.DAL;
using System.ComponentModel.DataAnnotations.Schema;

namespace BugTicketingSystem.BL;

public class UserReadDto
{
    public Guid Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string EmailAddress { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public ICollection<BugChildDto> Bugs { get; set; } = new HashSet<BugChildDto>();
    public ICollection<RoleChildDto> Roles { get; set; } = new HashSet<RoleChildDto>();
}
public class BugChildDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public String Status { get; set; } = string.Empty;
    public Guid ProjectId { get; set; }
}
public class RoleChildDto
{
    public Guid Id { get; set; }
    public string RoleName { get; set; } = RoleEnum.Tester.ToString();
}
