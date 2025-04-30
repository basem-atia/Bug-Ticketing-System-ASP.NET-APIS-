
using BugTicketingSystem.DAL;

namespace BugTicketingSystem.BL;

public class ProjectManager : IProjectManager
{
    private readonly IUnitOfWork _unitOfWork;

    public ProjectManager(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<GeneralResult> AddProject(ProjectAddDto project)
    {
        var existProject = await _unitOfWork.ProjectRepository.GetByName(project.Name);
        if (existProject != null)
        {
            return Functions.Error("404", "Project already used");
            //return new GeneralResult
            //{
            //    Success = false,
            //    Errors = [new ResultError { Code = "404", Message = "Project already used" }]
            //};
        }
        var projectToAdd = new Project
        {
            Id = Guid.NewGuid(),
            Name = project.Name,
            Description = project.Description,
        };
        _unitOfWork.ProjectRepository.AddProjectAsync(projectToAdd);
        await _unitOfWork.SaveChangesAsync();
        return Functions.Success<string>(projectToAdd.Id.ToString());
        //return new GeneralResult<string>
        //{
        //    Success = true,
        //    Errors = [],
        //    Data = projectToAdd.Id.ToString()
        //};
    }

    public async Task<GeneralResult> GetAllProject()
    {
        var initialprojects = await _unitOfWork.ProjectRepository.GetAllProjectsAsync();
        var projects = initialprojects.Select(p => new ProjectReadDto
        {
            Id = p.Id,
            Name = p.Name,
            Description = p.Description
        }).ToList();
        return Functions.Success<List<ProjectReadDto>>(projects);
        //return new GeneralResult<List<ProjectReadDto>>
        //{
        //    Success = true,
        //    Errors = [],
        //    Data = projects
        //};
    }

    public async Task<GeneralResult> GetAllProjectsWithBugs()
    {
        var initialprojects = await _unitOfWork.ProjectRepository.GetAllProjectsAsyncWithBugs();
        var projects = initialprojects.Select(p => new ProjectReadDto
        {
            Id = p.Id,
            Name = p.Name,
            Description = p.Description,
            Bugs = p.Bugs.Select(b => new BugChildDto
            {
                Id = b.Id,
                Title = b.Title,
                Description = b.Description,
                Status = b.Status,
                ProjectId = b.ProjectId
            }).ToList()
        }).ToList();
        return Functions.Success<List<ProjectReadDto>>(projects);
        //return new GeneralResult<List<ProjectReadDto>>
        //{
        //    Success = true,
        //    Errors = [],
        //    Data = projects
        //};
    }
    public async Task<GeneralResult> GetById(Guid id)
    {
        var initialproject = await _unitOfWork.ProjectRepository.GetProjectAsync(id);
        if (initialproject is null)
        {
            return Functions.Error("401", "Project is not found");
            //return new GeneralResult
            //{
            //    Success = false,
            //    Errors = [new ResultError { Code = "401", Message = "Project is not found" }]
            //};
        }
        var project = new ProjectReadDto
        {
            Id = initialproject.Id,
            Name = initialproject.Name,
            Description = initialproject.Description,
            Bugs = initialproject.Bugs.Select(b => new BugChildDto
            {
                Id = b.Id,
                Title = b.Title,
                Description = b.Description,
                Status = b.Status,
                ProjectId = b.ProjectId
            }).ToList()
        };
        return Functions.Success<ProjectReadDto>(project);
        //return new GeneralResult<ProjectReadDto>
        //{
        //    Success = true,
        //    Errors = [],
        //    Data = project
        //};
    }
}
