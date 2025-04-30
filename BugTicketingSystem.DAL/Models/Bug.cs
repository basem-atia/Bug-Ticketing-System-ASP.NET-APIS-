using System.ComponentModel.DataAnnotations.Schema;

namespace BugTicketingSystem.DAL
{
    public class Bug
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public String Status { get; set; } = string.Empty;
        [ForeignKey(nameof(Project))]
        public Guid ProjectId { get; set; }
        public Project Project { get; set; } = null!;
        public ICollection<User> Users { get; set; } = new HashSet<User>();
        public ICollection<FileAttachment> FileAttachments { get; set; } = new HashSet<FileAttachment>();


    }
}
