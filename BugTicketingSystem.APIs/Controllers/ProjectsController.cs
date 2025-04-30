using BugTicketingSystem.BL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BugTicketingSystem.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class ProjectsController : ControllerBase
    {
        private readonly IProjectManager _projectManager;

        public ProjectsController(IProjectManager projectManager)
        {
            _projectManager = projectManager;
        }

        [HttpGet]
        public async Task<ActionResult<GeneralResult>> GetAllProjects()
        {
            GeneralResult result = await _projectManager.GetAllProject();
            List<ProjectReadDto> projects = ((GeneralResult<List<ProjectReadDto>>)result).Data!;
            if (projects != null)
            {
                return Ok(Functions.Success<List<ProjectReadDto>>(projects));
            }
            return BadRequest(Functions.Fail(result.Errors));
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<GeneralResult>> GetProjectById([FromRoute] Guid id)
        {
            var result = await _projectManager.GetById(id);
            if (result.Success == true)
            {
                var project = ((GeneralResult<ProjectReadDto>)result).Data!;
                return Ok(Functions.Success<ProjectReadDto>(project));
            }
            return BadRequest(Functions.Fail(result.Errors));
        }
        [HttpPost]
        public async Task<ActionResult<GeneralResult>> AddProject([FromBody] ProjectAddDto projectAdd)
        {
            var result = await _projectManager.AddProject(projectAdd);
            if (result.Success == true)
            {
                var Addedproject = ((GeneralResult<string>)result).Data;
                return CreatedAtAction(nameof(GetProjectById), new { id = Addedproject }, projectAdd);
            }
            return BadRequest(Functions.Fail(result.Errors));
        }

    }
}
