using BugTicketingSystem.DAL;

namespace BugTicketingSystem.BL;

public class ProjectAddDto
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}
