using FluentValidation;

namespace AuthService.WebAPI.Controllers.Auth;

public record RegisterRequest(string Username, string Password, string ConfirmPassword);
public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
{
    public RegisterRequestValidator()
    {
        RuleFor(x => x.Username).NotEmpty();
        RuleFor(x => x.Password).NotEmpty();
        RuleFor(x => x.ConfirmPassword).NotEmpty();
        RuleFor(x => x.ConfirmPassword).Equal(x => x.Password);
    }
}