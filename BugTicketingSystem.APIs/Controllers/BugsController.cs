using BugTicketingSystem.BL;
using BugTicketingSystem.BL.Managers.Bugs;
using BugTicketingSystem.DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BugTicketingSystem.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class BugsController : ControllerBase
    {
        private readonly IBugManager _bugManager;

        public BugsController(IBugManager manager)
        {
            _bugManager = manager;
        }

        [HttpGet]
        public async Task<ActionResult<GeneralResult>> GetAllBugs()
        {
            GeneralResult result = await _bugManager.GetAllBug();
            List<BugReadDto> bugs = ((GeneralResult<List<BugReadDto>>)result).Data!;
            if (bugs != null)
            {
                return Ok(new GeneralResult<List<BugReadDto>>
                {
                    Success = true,
                    Data = bugs,
                    Errors = []
                });
            }
            return BadRequest(new GeneralResult { Success = false, Errors = result.Errors });
        }
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<GeneralResult>> GetBugById([FromRoute] Guid id)
        {
            var result = await _bugManager.GetBugByIdWithDetails(id);
            if (result.Success == true)
            {
                var Bug = ((GeneralResult<BugReadDto>)result).Data!;
                return Ok(Functions.Success<BugReadDto>(Bug));
            }
            return BadRequest(Functions.Fail(result.Errors));
        }
        [HttpPost]
        public async Task<ActionResult<GeneralResult>> AddBug([FromBody] BugAddDto bugAdd)
        {
            var result = await _bugManager.AddBug(bugAdd);
            if (result.Success == true)
            {
                var Addedbug = ((GeneralResult<string>)result).Data;
                return CreatedAtAction(nameof(GetBugById), new { id = Addedbug }, bugAdd);
            }
            return BadRequest(Functions.Fail(result.Errors));
        }
        [HttpPost("{id}/assignees")]
        public async Task<ActionResult<GeneralResult>> AssignUserToBug([FromBody] AssignUserDto UserId, [FromRoute(Name = "id")] Guid bugId)
        {
            var result = await _bugManager.AddUserToBug(UserId.UserId, bugId);
            if (result.Success == true)
            {
                string msg = ((GeneralResult<string>)result).Data!;
                return Ok(Functions.Success<string>(msg));
            }
            return BadRequest(Functions.Fail(result.Errors));
        }

        [HttpDelete("{id}/assignees/{userId}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<GeneralResult>> RemoveUserFromBug
            ([FromRoute(Name = "userId")] Guid userId, [FromRoute(Name = "id")] Guid bugId)
        {
            var result = await _bugManager.RemoveUserFromBug(userId, bugId);
            if (result.Success == true)
            {
                string msg = ((GeneralResult<string>)result).Data!;
                return Ok(Functions.Success<string>(msg));
            }
            return BadRequest(Functions.Fail(result.Errors));
        }

        [HttpPost("{id}/attachments")]
        public async Task<ActionResult<GeneralResult>> UploadAttachmentAsync([FromRoute] Guid id, [FromForm] IFormFile file)
        {
            var result = await _bugManager.UploadAttachmentAsync(id, file);
            if (result.Success == true)
            {
                string msg = ((GeneralResult<string>)result).Data!;
                return Ok(Functions.Success<string>(msg));
            }
            return BadRequest(Functions.Fail(result.Errors));
        }

        [HttpGet("{id}/attachments")]
        public async Task<ActionResult<GeneralResult>> GetAttachmentForBug([FromRoute] Guid id)
        {
            var result = await _bugManager.GetBugByIdWithAttachments(id);
            if (result.Success == true)
            {
                BugWithAttachmentDto bug = ((GeneralResult<BugWithAttachmentDto>)result).Data!;
                return Ok(Functions.Success<BugWithAttachmentDto>(bug));
            }
            return BadRequest(Functions.Fail(result.Errors));
        }

        [HttpDelete("{id}/attachments/{attachmentId}")]
        public async Task<ActionResult<GeneralResult>> DeleteAttachmentFromBug
            ([FromRoute(Name = "id")] Guid bugId, [FromRoute(Name = "attachmentId")] Guid AttachmentId)
        {
            var result = await _bugManager.RemoveAttachmentFromBug(AttachmentId, bugId);
            if (result.Success == true)
            {
                string msg = ((GeneralResult<string>)result).Data!;
                return Ok(Functions.Success<string>(msg));
            }
            return BadRequest(Functions.Fail(result.Errors));
        }
    }
}
