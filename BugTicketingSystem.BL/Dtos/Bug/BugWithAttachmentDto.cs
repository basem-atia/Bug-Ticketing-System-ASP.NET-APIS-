namespace BugTicketingSystem.BL;


public class BugWithAttachmentDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public String Status { get; set; } = string.Empty;
    public Guid ProjectId { get; set; }
    public ICollection<FileChildDto> FileAttachments { get; set; } = new HashSet<FileChildDto>();
}


