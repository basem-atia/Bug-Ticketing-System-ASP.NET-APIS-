using BugTicketingSystem.DAL;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BugTicketingSystem.BL;

public class BugReadDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public String Status { get; set; } = string.Empty;
    public Guid ProjectId { get; set; }
    public ICollection<UserChildDto> Users { get; set; } = new HashSet<UserChildDto>();
    public ICollection<FileChildDto> FileAttachments { get; set; } = new HashSet<FileChildDto>();
}
public class UserChildDto
{
    public Guid Id { get; set; }
    public string FullName { get; set; } = string.Empty;
}
public class FileChildDto
{
    public string FileName { get; set; } = string.Empty;
    public string FilePath { get; set; } = string.Empty;
    public DateTime UploadedAt { get; set; }
}
