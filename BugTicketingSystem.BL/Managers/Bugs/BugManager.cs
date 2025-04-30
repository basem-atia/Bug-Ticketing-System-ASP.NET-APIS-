using BugTicketingSystem.BL.Managers.Bugs;
using BugTicketingSystem.DAL;
using Microsoft.AspNetCore.Http;


namespace BugTicketingSystem.BL;

public class BugManager : IBugManager
{
    private readonly IUnitOfWork _unitOfWork;

    public BugManager(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<GeneralResult> AddBug(BugAddDto bugAdd)
    {
        var bug = new Bug
        {
            Id = Guid.NewGuid(),
            Title = bugAdd.Title,
            Description = bugAdd.Description,
            Status = bugAdd.Status,
            ProjectId = bugAdd.ProjectId,
        };
        _unitOfWork.BugRepository.AddBugAsync(bug);
        await _unitOfWork.SaveChangesAsync();
        return Functions.Success<string>(bug.Id.ToString());
        //return new GeneralResult<string>
        //{
        //    Success = true,
        //    Data = bug.Id.ToString(),
        //    Errors = []
        //};
    }

    public async Task<GeneralResult> GetAllBug()
    {
        var initialbugs = await _unitOfWork.BugRepository.GetAllBugsAsync();
        var bugs = initialbugs.Select(b => new BugReadDto
        {
            Id = b.Id,
            Title = b.Title,
            Description = b.Description,
            Status = b.Status,
            ProjectId = b.ProjectId
        }).ToList();
        return Functions.Success<List<BugReadDto>>(bugs);
        //return new GeneralResult<List<BugReadDto>>
        //{
        //    Success = true,
        //    Errors = [],
        //    Data = bugs
        //};
    }

    public async Task<GeneralResult> GetBugByIdWithDetails(Guid id)
    {
        var intialBug = await _unitOfWork.BugRepository.GetBugAsync(id);

        if (intialBug is null)
        {
            return Functions.Error("404", "Bug Not Found");
            //return new GeneralResult
            //{
            //    Success = false,
            //    Errors = [new ResultError
            //    { Code="404",Message= "Bug Not Found" }]
            //};
        }
        var bug = new BugReadDto
        {
            Id = intialBug.Id,
            Title = intialBug.Title,
            Description = intialBug.Description,
            Status = intialBug.Status,
            ProjectId = intialBug.ProjectId,
            Users = intialBug.Users.Select(
                u => new UserChildDto
                {
                    Id = u.Id,
                    FullName = u.FullName
                }
                ).ToList(),
            FileAttachments = intialBug.FileAttachments.Select(
                f => new FileChildDto
                {
                    FileName = f.FileName,
                    FilePath = f.FilePath,
                    UploadedAt = f.UploadedAt
                }
                ).ToList()
        };
        return Functions.Success<BugReadDto>(bug);
        //return new GeneralResult<BugReadDto>
        //{
        //    Success = true,
        //    Errors = [],
        //    Data = bug
        //};
    }

    public async Task<GeneralResult> AddUserToBug(Guid userId, Guid bugId)
    {
        var bug = await _unitOfWork.BugRepository.GetBugAsync(bugId);
        if (bug is null)
        {
            return Functions.Error("404", "Bug is not found");
            //return new GeneralResult
            //{
            //    Success = false,
            //    Errors = [new ResultError { Code = "404", Message = "Bug is not found" }]
            //};
        }
        var user = await _unitOfWork.UserRepository.GetUserByIdAsync(userId);
        if (user is null)
        {
            return Functions.Error("404", "User is not found");
            //return new GeneralResult
            //{
            //    Success = false,
            //    Errors = [new ResultError { Code = "404", Message = "User is not found" }]
            //};
        }
        if (bug.Users.Any(u => u.Id == userId))
        {
            return Functions.Error("409", "User already assigned to this bug");
            //return new GeneralResult
            //{
            //    Success = false,
            //    Errors = [new ResultError { Code = "409", Message = "User already assigned to this bug" }]
            //};
        }
        bug.Users.Add(user);
        await _unitOfWork.SaveChangesAsync();
        return Functions.Success<string>("User assigned successfully to bug");
        //return new GeneralResult<string>
        //{
        //    Success = true,
        //    Errors = [],
        //    Data = "User assigned successfully to bug"
        //};
    }

    public async Task<GeneralResult> RemoveUserFromBug(Guid userId, Guid bugId)
    {
        var bug = await _unitOfWork.BugRepository.GetBugAsync(bugId);
        if (bug is null)
        {
            return Functions.Error("404", "Bug is not found");
            //return new GeneralResult
            //{
            //    Success = false,
            //    Errors = [new ResultError { Code = "404", Message = "Bug is not found" }]
            //};
        }
        var user = await _unitOfWork.UserRepository.GetUserByIdAsync(userId);
        if (user is null)
        {
            return Functions.Error("404", "User is not found");
            //return new GeneralResult
            //{
            //    Success = false,
            //    Errors = [new ResultError { Code = "404", Message = "User is not found" }]
            //};
        }
        var assignedUser = bug.Users.FirstOrDefault(u => u.Id == userId);
        if (assignedUser is null)
        {
            return Functions.Error("404", "User not exist in this bug");
            //return new GeneralResult
            //{
            //    Success = false,
            //    Errors = [new ResultError { Code = "404", Message = "User not exist in this bug" }]
            //};
        }
        bug.Users.Remove(user);
        await _unitOfWork.SaveChangesAsync();
        return Functions.Success<string>("User Removed successfully From This bug");
        //return new GeneralResult<string>
        //{
        //    Success = true,
        //    Errors = [],
        //    Data = "User Removed successfully From This bug"
        //};
    }

    public async Task<GeneralResult> UploadAttachmentAsync(Guid bugId, IFormFile file)
    {
        var bug = await _unitOfWork.BugRepository.GetBugAsync(bugId);
        if (bug is null)
        {
            return Functions.Error("404", "Bug is not found");
            //return new GeneralResult
            //{
            //    Success = false,
            //    Errors = [new ResultError { Code = "404", Message = "Bug is not found" }]
            //};
        }
        if (file is null || file.Length == 0)
        {
            return Functions.Error("400", "File is empty");
            //return new GeneralResult
            //{
            //    Success = false,
            //    Errors = [new ResultError { Code = "400", Message = "File is empty" }]
            //};
        }
        var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".pdf", ".avif" };
        if (!allowedExtensions.Contains(Path.GetExtension(file.FileName)))
        {
            return Functions.Error("400", "Invalid file type");
            //return new GeneralResult
            //{
            //    Success = false,
            //    Errors = [new ResultError { Code = "400", Message = "Invalid file type" }]
            //};
        }

        if (file.Length > 15 * 1024 * 1024)
        {
            return Functions.Error("400", "File size exceeds the limit");
            //return new GeneralResult
            //{
            //    Success = false,
            //    Errors = [new ResultError { Code = "400", Message = "File size exceeds the limit" }]
            //};
        }
        var uploadFolder = Path.Combine("wwwroot", "Attachments", bugId.ToString());
        Directory.CreateDirectory(uploadFolder);
        var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
        var filePath = Path.Combine(uploadFolder, fileName);
        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }
        var attachment = new FileAttachment
        {
            Id = Guid.NewGuid(),
            FileName = file.FileName,
            FilePath = filePath,
            UploadedAt = DateTime.UtcNow,
            BugId = bugId
        };
        bug.FileAttachments.Add(attachment);
        _unitOfWork.FileAttachmentRepository.AddAttachmentAsync(attachment);
        await _unitOfWork.SaveChangesAsync();
        return Functions.Success<string>("Attachment Saved Successfully and Added To Bug");
        //return new GeneralResult<string>
        //{
        //    Success = true,
        //    Errors = [],
        //    Data = "Attachment Saved Successfully and Added To Bug"
        //};
    }

    public async Task<GeneralResult> GetBugByIdWithAttachments(Guid id)
    {
        var intialBug = await _unitOfWork.BugRepository.GetBugAsync(id);

        if (intialBug is null)
        {
            return Functions.Error("404", "Bug Not Found");
            //return new GeneralResult
            //{
            //    Success = false,
            //    Errors = [new ResultError
            //      { Code="404",Message= "Bug Not Found" }]
            //};
        }
        var bug = new BugWithAttachmentDto
        {
            Id = intialBug.Id,
            Title = intialBug.Title,
            Description = intialBug.Description,
            Status = intialBug.Status,
            ProjectId = intialBug.ProjectId,
            FileAttachments = intialBug.FileAttachments.Select(
                f => new FileChildDto
                {
                    FileName = f.FileName,
                    FilePath = f.FilePath,
                    UploadedAt = f.UploadedAt
                }
                ).ToList()
        };
        return Functions.Success<BugWithAttachmentDto>(bug);
        //return new GeneralResult<BugWithAttachmentDto>
        //{
        //    Success = true,
        //    Errors = [],
        //    Data = bug
        //};
    }

    public async Task<GeneralResult> RemoveAttachmentFromBug(Guid AttachmentId, Guid bugId)
    {
        var bug = await _unitOfWork.BugRepository.GetBugAsync(bugId);
        if (bug is null)
        {
            return Functions.Error("404", "Bug is not found");
            //return new GeneralResult
            //{
            //    Success = false,
            //    Errors = [new ResultError { Code = "404", Message = "Bug is not found" }]
            //};
        }
        var file = await _unitOfWork.FileAttachmentRepository.GetAttachmentAsync(AttachmentId);
        if (file is null)
        {
            return Functions.Error("404", "Attachment is not found");
            //return new GeneralResult
            //{
            //    Success = false,
            //    Errors = [new ResultError { Code = "404", Message = "Attachment is not found" }]
            //};
        }
        var assignedFile = bug.FileAttachments.FirstOrDefault(u => u.Id == AttachmentId);
        if (assignedFile is null)
        {
            return Functions.Error("404", "Attachment not exist in this bug");
            //return new GeneralResult
            //{
            //    Success = false,
            //    Errors = [new ResultError { Code = "404", Message = "Attachment not exist in this bug" }]
            //};
        }
        var filePath = assignedFile.FilePath;
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }
        bug.FileAttachments.Remove(assignedFile);
        await _unitOfWork.SaveChangesAsync();
        return Functions.Success<string>("Attachment Removed successfully From This bug");
        //return new GeneralResult<string>
        //{
        //    Success = true,
        //    Errors = [],
        //    Data = "Attachment Removed successfully From This bug"
        //};
    }


}
