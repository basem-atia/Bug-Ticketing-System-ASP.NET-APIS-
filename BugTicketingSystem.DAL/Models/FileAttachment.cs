using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BugTicketingSystem.DAL
{
    public class FileAttachment
    {
        [Key]
        public Guid Id { get; set; }
        public string FileName { get; set; } = string.Empty;
        public string FilePath { get; set; } = string.Empty;

        public DateTime UploadedAt { get; set; }

        [ForeignKey(nameof(Bug))]
        public Guid BugId { get; set; }
        public Bug Bug { get; set; } = null!;
    }
}
