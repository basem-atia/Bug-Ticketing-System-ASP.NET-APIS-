using BugTicketingSystem.DAL;

namespace BugTicketingSystem.BL;

public class ProjectReadDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public ICollection<BugChildDto>? Bugs { get; set; } = new HashSet<BugChildDto>();
}

