using Microsoft.AspNetCore.Http;

namespace BugTicketingSystem.BL.Managers.Bugs
{
    public interface IBugManager
    {
        Task<GeneralResult> AddBug(BugAddDto bugAdd);
        Task<GeneralResult> GetBugByIdWithDetails(Guid id);
        Task<GeneralResult> GetAllBug();
        Task<GeneralResult> AddUserToBug(Guid userId, Guid bugId);
        Task<GeneralResult> RemoveUserFromBug(Guid userId, Guid bugId);
        Task<GeneralResult> UploadAttachmentAsync(Guid bugId, IFormFile file);
        Task<GeneralResult> GetBugByIdWithAttachments(Guid id);
        Task<GeneralResult> RemoveAttachmentFromBug(Guid AttachmentId, Guid bugId);

    }
}
