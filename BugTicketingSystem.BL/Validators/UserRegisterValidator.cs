using BugTicketingSystem.DAL;
using FluentValidation;

namespace BugTicketingSystem.BL;

public class UserRegisterValidator : AbstractValidator<RegisterDto>
{
    private readonly IUnitOfWork _unitOfWork;

    public UserRegisterValidator(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        RuleFor(u => u.FullName)
                    .NotEmpty()
                    .WithMessage("Name cannot be empty")
                    .WithErrorCode("ERR-01")
                    .MinimumLength(3)
                    .WithMessage("Name cannot be shorter than 3 characters")
                    .WithErrorCode("ERR-02")
                    .MaximumLength(100)
                    .WithMessage("Name cannot be longer than 100 characters")
                    .WithErrorCode("ERR-03");
        RuleFor(u => u.EmailAddress)
                    .NotEmpty()
                    .WithMessage("email cannot be empty")
                    .WithErrorCode("ERR-04")
                    .EmailAddress()
                    .WithMessage("invalid email format")
                    .WithErrorCode("ERR-05")
                    .MustAsync(uniqueEmail)
                    .WithMessage("Email is already taken")
                    .WithErrorCode("ERR-06");
        RuleFor(u => u.Password)
                    .NotEmpty()
                    .WithMessage("Password is required")
                    .WithErrorCode("ERR-07")
                    .MinimumLength(6)
                    .WithMessage("Password must be at least 6 characters")
                    .WithErrorCode("ERR-08")
                    .Matches(@"[A-Z]")
                    .WithMessage("Password must contain at least one uppercase letter")
                    .WithErrorCode("ERR-09")
                    .Matches(@"[a-z]")
                    .WithMessage("Password must contain at least one lowercase letter")
                    .WithErrorCode("ERR-10")
                    .Matches(@"\d")
                    .WithMessage("Password must contain at least one digit")
                    .WithErrorCode("ERR-11")
                    .Matches(@"[\!\@\#\$\%\^\&\*\(\)\-\+\=]")
                    .WithMessage("Password must contain at least one special character")
                    .WithErrorCode("ERR-12");
    }

    private async Task<bool> uniqueEmail(string email, CancellationToken token)
    {
        var user = await _unitOfWork.UserRepository.GetUserByEmailAsync(email);
        return user == null;
    }
}
