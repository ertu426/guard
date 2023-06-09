using FluentValidation;

namespace AuthService.WebAPI.Controllers.Auth;

public record LoginRequest(string Username, string Password);
public class LoginRequestValidator : AbstractValidator<LoginRequest>
{
    public LoginRequestValidator()
    {
        RuleFor(x => x.Username).NotEmpty();
        RuleFor(x => x.Password).NotEmpty();
    }
}