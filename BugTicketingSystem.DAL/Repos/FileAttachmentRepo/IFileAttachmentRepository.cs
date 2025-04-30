namespace BugTicketingSystem.DAL;

public interface IFileAttachmentRepository
{
    Task<IEnumerable<FileAttachment>> GetAllAttachmentsAsync();
    Task<FileAttachment?> GetAttachmentAsync(Guid id);
    void AddAttachmentAsync(FileAttachment fileAttachment);
    void UpdateAttachmentsAsync(FileAttachment fileAttachment);
    void DeleteAttachmentsAsync(FileAttachment fileAttachment);
}
