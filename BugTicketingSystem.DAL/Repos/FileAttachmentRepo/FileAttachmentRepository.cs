
using Microsoft.EntityFrameworkCore;

namespace BugTicketingSystem.DAL;

public class FileAttachmentRepository : IFileAttachmentRepository
{
    private readonly BugTicketingContext _context;

    public FileAttachmentRepository(BugTicketingContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<FileAttachment>> GetAllAttachmentsAsync()
    {
        return await _context.Set<FileAttachment>().AsNoTracking().ToListAsync();
    }

    public async Task<FileAttachment?> GetAttachmentAsync(Guid id)
    {
        return await _context.Set<FileAttachment>().FindAsync(id);
    }

    public void AddAttachmentAsync(FileAttachment fileAttachment)
    {
        _context.Set<FileAttachment>().Add(fileAttachment);
    }

    public void DeleteAttachmentsAsync(FileAttachment fileAttachment)
    {
        _context.Set<FileAttachment>().Remove(fileAttachment);
    }

    public void UpdateAttachmentsAsync(FileAttachment fileAttachment)
    {
        //_context.Set<FileAttachment>().Update(fileAttachment);
    }
}
