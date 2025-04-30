using BugTicketingSystem.DAL;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BugTicketingSystem.BL;

public class UserManager : IUserManager
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IConfiguration _configuration;

    public UserManager(IUnitOfWork unitOfWork, IConfiguration configuration)
    {
        _unitOfWork = unitOfWork;
        _configuration = configuration;
    }

    public async Task<GeneralResult> Login(LoginDto user)
    {
        User? Existuser = await _unitOfWork.UserRepository.GetUserByEmailAsync(user.EmailAddress);
        if (Existuser is null)
        {
            return Functions.Error("401", "user not found");
            //return new GeneralResult
            //{
            //    Success = false,
            //    Errors = [new ResultError { Code = "401", Message = "user not found" }],
            //};
        }
        bool result = BCrypt.Net.BCrypt.Verify(user.Password, Existuser.Password);

        if (!result)
        {
            return Functions.Error("401", "invalid credentials");
            //return new GeneralResult
            //{
            //    Success = false,
            //    Errors = [new ResultError { Code = "401", Message = "invalid credentials" }],
            //};
        }
        var claims = new List<Claim>();
        if (Existuser.Roles.Select(r => r.RoleName).Contains(RoleEnum.Developer.ToString()))
        {
            claims.AddRange(
                new Claim(ClaimTypes.NameIdentifier, Existuser.Id.ToString()),
                new Claim(ClaimTypes.Role, "Developer"));
        }
        else if (Existuser.Roles.Select(r => r.RoleName).Contains(RoleEnum.Admin.ToString()))
        {
            claims.AddRange(
                new Claim(ClaimTypes.NameIdentifier, Existuser.Id.ToString()),
                new Claim(ClaimTypes.Role, "Admin"));
        }
        else
        {
            claims.AddRange(
                new Claim(ClaimTypes.NameIdentifier, Existuser.Id.ToString()),
                new Claim(ClaimTypes.Role, "Tester"));
        }

        TokenDto token = GenerateToken(claims);
        return Functions.Success<TokenDto>(token);
        //return new GeneralResult<TokenDto>
        //{
        //    Success = true,
        //    Errors = [],
        //    Data = token
        //};
    }
    private TokenDto GenerateToken(List<Claim> claims)
    {
        var secretKey = _configuration["SecretKey"]!;
        var secretKeyInBytes = Encoding.UTF8.GetBytes(secretKey);
        var key = new SymmetricSecurityKey(secretKeyInBytes);
        var token = new JwtSecurityToken(
            issuer: _configuration["jwt:Issuer"],
            audience: _configuration["jwt:Audience"],
            claims: claims,
            notBefore: DateTime.UtcNow,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));
        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
        return new TokenDto { token = tokenString, ExpiryDate = token.ValidTo };
    }
    public async Task<GeneralResult> Register(RegisterDto registerUser)
    {
        var exist = await _unitOfWork.UserRepository.GetUserByEmailAsync(registerUser.EmailAddress);
        if (exist != null)
        {
            return Functions.Error("409", "Email already used");
            //return new GeneralResult<string>
            //{
            //    Success = false,
            //    Errors = [new ResultError { Code = "404", Message = "Email already used" }]
            //};
        }
        var user = new User
        {
            Id = Guid.NewGuid(),
            FullName = registerUser.FullName,
            EmailAddress = registerUser.EmailAddress.ToLower()
        };

        Role role = await _unitOfWork.RoleRepository.GetFullRoleAsync(RoleEnum.Tester.ToString());
        if (role != null)
        {
            user.Roles.Add(role);
        }
        user.Password = BCrypt.Net.BCrypt.HashPassword(registerUser.Password);
        _unitOfWork.UserRepository.AddUserAsync(user);
        await _unitOfWork.SaveChangesAsync();
        return Functions.Success<string>(user.Id.ToString());
        //return new GeneralResult<string>
        //{
        //    Success = true,
        //    Errors = [],
        //    Data = user.id.ToString()
        //};
    }
    public async Task<GeneralResult> AdminRegister(RegisterDto registerUser)
    {
        var exist = await _unitOfWork.UserRepository.GetUserByEmailAsync(registerUser.EmailAddress);
        if (exist != null)
        {
            return Functions.Error("409", "Email already used");
            //return new GeneralResult<string>
            //{
            //    Success = false,
            //    Errors = [new ResultError { Code = "404", Message = "Email already used" }]
            //};
        }
        var user = new User
        {
            Id = Guid.NewGuid(),
            FullName = registerUser.FullName,
            EmailAddress = registerUser.EmailAddress.ToLower()
        };

        Role role = await _unitOfWork.RoleRepository.GetFullRoleAsync(RoleEnum.Admin.ToString());
        if (role != null)
        {
            user.Roles.Add(role);
        }
        user.Password = BCrypt.Net.BCrypt.HashPassword(registerUser.Password);
        _unitOfWork.UserRepository.AddUserAsync(user);
        await _unitOfWork.SaveChangesAsync();
        return Functions.Success<string>(user.Id.ToString());
        //return new GeneralResult<string>
        //{
        //    Success = true,
        //    Errors = [],
        //    Data = user.EmailAddress.ToString()
        //};
    }
    public async Task<GeneralResult> GetAll()
    {
        var intUsers = await _unitOfWork.UserRepository.GetAllUsersAsync();
        if (intUsers != null)
        {
            var users = intUsers.Select(u =>
                        new UserReadDto
                        {
                            Id = u.Id,
                            FullName = u.FullName,
                            EmailAddress = u.EmailAddress
                        }
                        ).ToList();
            return Functions.Success<List<UserReadDto>>(users);
            //return new GeneralResult<List<UserReadDto>>
            //{
            //    Success = true,
            //    Errors = [],
            //    Data = users
            //};
        }
        return Functions.Error("404", "users not found");
        //return new GeneralResult
        //{
        //    Success = false,
        //    Errors = [new ResultError { Code = "404", Message = "users not found" }]
        //};
    }
    public async Task<GeneralResult> GetById(string email)
    {
        User? user = await _unitOfWork.UserRepository.GetUserByEmailAsync(email);
        if (user is null)
        {
            return Functions.Error("404", "user not found");
            //return new GeneralResult
            //{
            //    Success = false,
            //    Errors = [new ResultError { Code = "401", Message = "user not found" }]
            //};
        }

        UserReadDto userRead = new UserReadDto
        {
            Id = user.Id,
            FullName = user.FullName,
            EmailAddress = user.EmailAddress,
            Password = user.Password,
            Bugs = user.Bugs.Select(b => new BugChildDto
            {
                Id = b.Id,
                Description = b.Description,
                Title = b.Title,
                Status = b.Status,
                ProjectId = b.ProjectId
            }).ToList(),
            Roles = user.Roles.Select(u => new RoleChildDto { Id = u.Id, RoleName = u.RoleName.ToString() }).ToList()

        };
        return Functions.Success<UserReadDto>(userRead);
        //return new GeneralResult<UserReadDto> { Success = true, Errors = [], Data = userRead };
    }
}
