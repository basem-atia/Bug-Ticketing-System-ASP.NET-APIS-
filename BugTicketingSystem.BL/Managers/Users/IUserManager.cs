using BugTicketingSystem.DAL;

namespace BugTicketingSystem.BL;

public interface IUserManager
{
    Task<GeneralResult> Register(RegisterDto user);
    Task<GeneralResult> AdminRegister(RegisterDto registerUser);
    Task<GeneralResult> Login(LoginDto user);
    Task<GeneralResult> GetById(string email);
    Task<GeneralResult> GetAll();

}
