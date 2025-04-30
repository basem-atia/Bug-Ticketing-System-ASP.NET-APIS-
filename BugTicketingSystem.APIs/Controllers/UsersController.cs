using BugTicketingSystem.BL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BugTicketingSystem.APIs.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class UsersController : ControllerBase
{
    private readonly IUserManager _userManager;
    private readonly UserRegisterValidator _rules;
    public UsersController(IUserManager userManager, UserRegisterValidator rules)
    {
        _userManager = userManager;
        _rules = rules;
    }
    [HttpGet]
    public async Task<ActionResult<GeneralResult>> GetAll()
    {
        var result = await _userManager.GetAll();
        if (result.Success == true)
        {
            var users = ((GeneralResult<List<UserReadDto>>)result).Data!;
            return Ok(Functions.Success<List<UserReadDto>>(users));
        }
        return NotFound(Functions.Fail(result.Errors));
    }
    [HttpGet("{email}")]
    public async Task<ActionResult<GeneralResult>> GetUser([FromRoute] string email)
    {
        GeneralResult result = await _userManager.GetById(email);
        if (result.Success == true)
        {
            var user = ((GeneralResult<UserReadDto>)result).Data!;
            return Ok(Functions.Success<UserReadDto>(user));

        }
        return NotFound(Functions.Fail(result.Errors));
    }
    [HttpPost("register")]
    public async Task<ActionResult<GeneralResult>> AddUser(RegisterDto user)
    {
        var validationResult = await _rules.ValidateAsync(user);
        if (!validationResult.IsValid)
        {

            return BadRequest(Functions.Fail(validationResult.Errors
                .Select(e => new ResultError
                {
                    Code = e.ErrorCode,
                    Message = e.ErrorMessage,
                    PropertyName = e.PropertyName,
                    AttemptedValue = e.AttemptedValue?.ToString()
                }).ToArray()));
        }
        var userEmail = await _userManager.Register(user);
        if (userEmail.Success == true)
        {

            var email = ((GeneralResult<string>)userEmail).Data;
            return CreatedAtAction(nameof(GetUser), new { email = email }, user);
        }
        return BadRequest(Functions.Fail(userEmail.Errors));
    }
    [HttpPost("register/admin")]
    public async Task<ActionResult<GeneralResult>> AddAdmin(RegisterDto admin)
    {
        var validationResult = await _rules.ValidateAsync(admin);
        if (!validationResult.IsValid)
        {
            return BadRequest(Functions.Fail(validationResult.Errors
                .Select(e => new ResultError
                {
                    Code = e.ErrorCode,
                    Message = e.ErrorMessage,
                    PropertyName = e.PropertyName,
                    AttemptedValue = e.AttemptedValue?.ToString()
                }).ToArray()));
        }
        var userEmail = await _userManager.AdminRegister(admin);
        if (userEmail.Success == true)
        {

            var email = ((GeneralResult<string>)userEmail).Data;
            return CreatedAtAction(nameof(GetUser), new { email = email }, admin);
        }
        return BadRequest(Functions.Fail(userEmail.Errors));
    }
    [HttpPost("login")]
    public async Task<ActionResult<GeneralResult>> Login(LoginDto user)
    {
        var result = await _userManager.Login(user);
        if (result.Success == true)
        {
            var token = ((GeneralResult<TokenDto>)result).Data!;
            return Ok(Functions.Success<TokenDto>(token));
        }
        return BadRequest(Functions.Fail(result.Errors));
    }
}
